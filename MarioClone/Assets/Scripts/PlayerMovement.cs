using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("Abilitky")]
    public bool doubleJump;

    public bool dash;
    public float dashSpeed;
    public float dashTime;

    private bool climbing;
    private float moveInput;
    private bool grounded;
    private bool stickyContact;
    private bool wallColision;
    private float moveInputVer;
    private bool jumping;
    private int jumpNumber;
    private bool isDashing;
    private float startDashing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startDashing = dashTime;
    }

    void Update()
    {
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

            if (Input.GetKeyDown(KeyCode.LeftShift) && dash)
            {
                isDashing = true;
            }

            if (isDashing)
            {
                startDashing -= Time.deltaTime;
                rb.gravityScale = 0;
                rb.velocity = new Vector2(moveInput * dashSpeed, rb.velocity.y);
            } else
            {
                rb.velocity = new Vector2(moveInput * Speed, rb.velocity.y);
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

            if (Grounded())
            {
                jumpNumber = 1;
            }

            if (Input.GetButtonDown("Jump") && jumpNumber > 0 && doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpNumber--;
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
            } else
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