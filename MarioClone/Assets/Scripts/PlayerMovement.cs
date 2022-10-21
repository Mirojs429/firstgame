using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Nastavení rychlostí a síly")]
    public float jumpForce = 3f;
    public float Speed = 5f;
    public float wallSlideSpeed;
    public float timeToJump;

    [Header("Groun & wall check")]
    public Transform groundCheck;
    public Transform wallCheck;
    public Transform wallCheckLeft;
    public float groundedRadius = .5f;
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;

    [Header("Animace")]
    public Animator animator;

    [Header("Žebøík")]
    public LayerMask whatIsLadder;
    public float distance;

    [Header("Double Jump")]
    public Image DJMask;
    public bool doubleJump;
    public float doubleJumpCooldown;
    private float DJCooldownCount;
    private bool doubleJumpCooldownB = false;

    [Header("Dash")]
    public Image dashMask;
    public bool dash;
    public float dashSpeed;
    public float dashTime;
    public float dashCooldown;
    private float dashCooldownCount;
    private bool dashCooldownCountB = false;

    private bool climbing;
    private float moveInput;
    private bool grounded;
    private bool stickyContact;
    private bool wallColision;
    private float moveInputVer;
    private bool jumping;
    private int jumpNumber = 1;
    public static bool isDashing;
    private float startDashing;

    [Header("Player info")]
    public TMP_Text playerInfo;

    // -- Animation cotroller --
    private bool an_Idle; //graunded & moveinput = 0
    private bool an_Moving; //graunded & moveinput <> 0
    private bool an_Jump; //!grounded & velocityY > 0
    private bool an_DoubleJump; //!grounded & velocityY > 0 & pressed DoubleJump
    private bool an_Falling; //!grounded & velocityY < 0
    private bool an_Dash; //dashing
    private bool an_Ladder; //vertical input > 0 & climbing
    private int climbingSpeed;
    private bool an_WallGrab; //stickyContact & moveinput > 0

    // -- Sound Controller --

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startDashing = dashTime;
        DJCooldownCount = doubleJumpCooldown;
        dashCooldownCount = dashCooldown;
        
    }

    void Update()
    {
        

        if (!PauseMenu.pause)
        {
            AnimationControll();

            // -- Uložení hoizontálního a vertikálního inputu --
            moveInput = Input.GetAxisRaw("Horizontal");
            moveInputVer = Input.GetAxisRaw("Vertical");

            //sticky contack check
            wallColision = Physics2D.OverlapCircle(wallCheck.position, groundedRadius, whatIsWall);
            if (wallColision && moveInput != 0)
            {
                stickyContact = true;
            } else
            {
                Invoke("SetTouchingFalse", timeToJump);
            }

            
            // -- Dash --
            if (Input.GetKeyDown(KeyCode.LeftShift) && moveInput != 0 && dash && !dashCooldownCountB
                || Input.GetKeyDown(KeyCode.JoystickButton2) && moveInput != 0 && dash && !dashCooldownCountB)
            {
                isDashing = true;
                dashCooldownCountB = true;
                dashMask.fillAmount = 1;
            }
            // Dash cooldown
            if (dashCooldownCountB)
            {
                dashCooldownCount -= 1 * Time.deltaTime;
                dashMask.fillAmount -= 1 / dashCooldown * Time.deltaTime;
                if (dashCooldownCount <= 0)
                {
                    dashCooldownCountB = false;
                    dashCooldownCount = dashCooldown;
                }
            }

            // Dash pohyb
            if (isDashing)
            {
                startDashing -= Time.deltaTime;
                rb.gravityScale = 0;
                Physics2D.IgnoreLayerCollision(10, 11, true);
                Physics2D.IgnoreLayerCollision(10, 9, true);
                rb.velocity = new Vector2(moveInput * dashSpeed, 0f);
            } else
            {
                rb.velocity = new Vector2(moveInput * Speed, rb.velocity.y);
                Physics2D.IgnoreLayerCollision(10, 11, false);
                Physics2D.IgnoreLayerCollision(10, 9, false);
                rb.gravityScale = 3;
            }

            //Dash ukonèení
            if (startDashing <= 0)
            {
                startDashing = dashTime;
                isDashing = false;
            }

            //Otoèení postavy podle smìru pohybu
            if (moveInput < 0)
            {
                //gameObject.GetComponent<SpriteRenderer>().flipX = true;
                transform.eulerAngles = new Vector3(0, -180, 0);
            } else if (moveInput > 0)
            {
                //gameObject.GetComponent<SpriteRenderer>().flipX = false;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

            //Detekce skoku
            if(!Grounded() && !climbing && !stickyContact)
            {
                jumping = true;
                //animator.SetBool("IsJumping", true);
            } else
            {
                //animator.SetBool("IsJumping", false);
                jumping = false;
            }

            //Doublejump odpoèet
            if (doubleJumpCooldownB)
            {
                DJCooldownCount -= 1 * Time.deltaTime;
                DJMask.fillAmount -= 1 / doubleJumpCooldown * Time.deltaTime;
                if (DJCooldownCount <= 0)
                {
                    doubleJumpCooldownB = false;
                    DJCooldownCount = doubleJumpCooldown;
                    jumpNumber = 1;
                }
            }

            //Doublejump  a jump
            if (Input.GetButtonDown("Jump") && jumpNumber > 0 && doubleJump && !doubleJumpCooldownB && !Grounded() && !stickyContact && !isDashing)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                doubleJumpCooldownB = true;
                DJMask.fillAmount = 1;
                jumpNumber--;
            }
            else if (Input.GetButtonDown("Jump") && Grounded() && !isDashing)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            //Jump na sticky zemi
            if (Input.GetButtonDown("Jump") && stickyContact)
            {
                rb.velocity = new Vector2(jumpForce * -moveInput, jumpForce);
            }

            //Animace pohybu postavy
            if (moveInput != 0 && !stickyContact && !climbing && !jumping)
            {
                animator.SetBool("IsMoving", true);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }

            //Slide postavy po sticky zemi
            if (stickyContact && !Grounded() && moveInput != 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
                animator.SetBool("IsGrabing", true);
            }else
            {
                animator.SetBool("IsGrabing", false);
            }

            //Detekce žebøíku
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);
            if (hitInfo.collider != null)
            {
                if (moveInputVer != 0)
                {
                    climbing = true;
                }
            } else
            {
                climbing = false;
            }

            //Pohyb po žebøíku
            if (climbing)
            {
                rb.velocity = new Vector2(rb.velocity.x, moveInputVer * Speed);
                rb.gravityScale = 0;
                animator.SetBool("IsFadder", true);
            } else if (Grounded() || !climbing)
            {
                rb.gravityScale = 3;
                animator.SetBool("IsFadder", false);
            }
        }
    }

    void AnimationControll()
    {
        if(Grounded() && moveInput == 0 && !stickyContact && !climbing)
        {
            an_Idle = true;
            animator.Play("Player_idle");
        } else
        {
            an_Idle = false;
        }

        if (Grounded() && moveInput != 0 && !isDashing)
        {
            an_Moving = true;
            animator.Play("Player_walk");
        } else
        {
            an_Moving = false;
        }

        if (!Grounded() && rb.velocity.y > 1f && !stickyContact && !climbing && !isDashing)
        {
            an_Jump = true;
            animator.Play("Player_jump");
        } else
        {
            an_Jump = false;
        }

        if (Input.GetButtonDown("Jump") && jumpNumber > 0 && doubleJump && !doubleJumpCooldownB && !Grounded() && !stickyContact && !isDashing)
        {
            an_DoubleJump = true;
            animator.Play("Player_jump");
        } else
        {
            an_DoubleJump = false;
        }

        if (!Grounded() && rb.velocity.y < -1f && !stickyContact && !climbing && !isDashing)
        {
            an_Falling = true;
            animator.Play("Player_fall");
        }
        else
        {
            an_Falling = false;
        }

        if (isDashing)
        {
            an_Dash = true;
            animator.Play("Player_dash");
        }
        else
        {
            an_Dash = false;
        }

        if (climbing)
        {
            an_Ladder = true;
            animator.Play("Player_ladder");
        } else
        {
            an_Ladder = false;
            climbingSpeed = 0;
            animator.speed = 1;
        }

        if (climbing && moveInputVer != 0)
        {
            climbingSpeed = 1;
            animator.speed = 1;
        } else if (climbing && moveInputVer == 0)
        {
            climbingSpeed = 0;
            animator.speed = 0;
        }

        if (stickyContact)
        {
            an_WallGrab = true;
            animator.Play("Player_wallGrab");
        } else
        {
            an_WallGrab = false;
        }

        playerInfo.text =
            "--- Player info ---" + "\n\n" +
            "Idle: " + an_Idle + "\n" +
            "Moving: " + an_Moving + "\n" +
            "Jump: " + an_Jump + "\n" +
            "DoubleJump: " + an_DoubleJump + "\n" +
            "Falling: " + an_Falling + "\n" +
            "Dash: " + an_Dash + "\n" +
            "Ladder: " + an_Ladder + "\n" +
            "Ladder speed: " + climbingSpeed + "\n" +
            "Sticky: " + an_WallGrab + "\n";
    }

    void SetTouchingFalse()
    {
        stickyContact = false;
    }

    private bool Grounded()
    {
        bool wasGrounded = grounded;
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
            }
        }

        return grounded;
    }
}