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
    private int numOfDJ = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startDashing = dashTime;
        DJCooldownCount = doubleJumpCooldown;
        dashCooldownCount = dashCooldown;
        
    }

    void Update()
    {
        playerInfo.text =
            "--- Player info ---" + "\n\n" +
            "Dashing: " + isDashing + "\n" +
            "Gravity: " + rb.gravityScale + "\n" +
            "Doublejumps: " + numOfDJ;

        if (!PauseMenu.pause)
        {
            wallColision = Physics2D.OverlapCircle(wallCheck.position, groundedRadius, whatIsWall);
            if (wallColision && moveInput != 0)
            {
                stickyContact = true;
            } else
            {
                Invoke("SetTouchingFalse", timeToJump);
            }

            moveInput = Input.GetAxisRaw("Horizontal");

            if (Input.GetKeyDown(KeyCode.LeftShift) && moveInput != 0 && dash && !dashCooldownCountB
                || Input.GetKeyDown(KeyCode.JoystickButton2) && moveInput != 0 && dash && !dashCooldownCountB)
            {
                isDashing = true;
                dashCooldownCountB = true;
                dashMask.fillAmount = 1;
            }

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

            if (isDashing)
            {
                startDashing -= Time.deltaTime;
                rb.gravityScale = 0;
                Physics2D.IgnoreLayerCollision(10, 11, true);
                Physics2D.IgnoreLayerCollision(10, 9, true);
                rb.velocity = new Vector2(moveInput * dashSpeed, rb.velocity.y);
            } else
            {
                rb.velocity = new Vector2(moveInput * Speed, rb.velocity.y);
                Physics2D.IgnoreLayerCollision(10, 11, false);
                Physics2D.IgnoreLayerCollision(10, 9, false);
                //rb.gravityScale = 3;
            }

            if (startDashing <= 0)
            {
                startDashing = dashTime;
                isDashing = false;
                rb.gravityScale = 3;
            }

            if (moveInput < 0)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            } else if (moveInput > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }

            if(!Grounded() && !climbing && !stickyContact)
            {
                jumping = true;
                animator.SetBool("IsJumping", true);
            } else
            {
                animator.SetBool("IsJumping", false);
                jumping = false;
            }

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

            if (Input.GetButtonDown("Jump") && jumpNumber > 0 && doubleJump && !doubleJumpCooldownB && !Grounded() && !stickyContact)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                doubleJumpCooldownB = true;
                DJMask.fillAmount = 1;
                jumpNumber--;
                //DOublejump counter
                numOfDJ++;
            }
            else if (Input.GetButtonDown("Jump") && Grounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }

            if (Input.GetButtonDown("Jump") && stickyContact)
            {
                rb.velocity = new Vector2(jumpForce * -moveInput, jumpForce);
            }

            if (moveInput != 0 && !stickyContact && !climbing && !jumping)
            {
                animator.SetBool("IsMoving", true);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }

            if (stickyContact && !Grounded() && moveInput != 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
                animator.SetBool("IsGrabing", true);
            }else
            {
                animator.SetBool("IsGrabing", false);
            }

            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);
            moveInputVer = Input.GetAxisRaw("Vertical");
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

            if (climbing)
            {
                rb.velocity = new Vector2(rb.velocity.x, moveInputVer * Speed);
                rb.gravityScale = 0;
                animator.SetBool("IsFadder", true);
            } else if (Grounded())
            {
                rb.gravityScale = 3;
                animator.SetBool("IsFadder", false);
            }
        }
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