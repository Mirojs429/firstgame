using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public bool mustPatrol;
    public Rigidbody2D rb;
    public float speed;
    public Transform groundCheck, wallCheck, hitPointA, hitPointB;
    private bool mustFlip;
    public LayerMask whatIsGround, whatIsPlayer;

    public static bool kill;

    private bool hitA, hitB;

    void Start()
    {
        mustPatrol = true;
        kill = false;
    }


    void Update()
    {
        if (mustPatrol)
        {
            mustFlip = !Physics2D.OverlapCircle(groundCheck.position, 0.1f, whatIsGround);
            Patrol();
        }

        hitA = Physics2D.OverlapCircle(hitPointA.position, 0.1f, whatIsPlayer);
        hitB = Physics2D.OverlapCircle(hitPointB.position, 0.1f, whatIsPlayer);

        if (hitA || hitB)
        {
            if (PlayerMovement.isDashing == false)
            {
                kill = true;
            }
        }

    }

  /*  void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustFlip = !Physics2D.OverlapCircle(groundCheck.position, 0.1f, whatIsGround);
        }
    }*/

    void Patrol()
    {
        if (mustFlip || Physics2D.OverlapCircle(wallCheck.position, 0.1f, whatIsGround))
        {
            Flip();
        }
        rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        speed *= -1;
        mustPatrol = true;
    }
}
