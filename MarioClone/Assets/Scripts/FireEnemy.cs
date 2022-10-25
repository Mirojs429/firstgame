using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireEnemy : MonoBehaviour
{
    [HideInInspector] public bool mustPatrol;
    public Rigidbody2D rb;
    public float speed;
    public Transform groundCheck, wallCheck, hitPointA, hitPointB;
    private bool mustFlip;
    public LayerMask whatIsGround, whatIsPlayer;

    private bool hitA, hitB;

    [Header("Groun & wall check")]
    private Transform player;
    private GameObject playerGO;
    public float range;
    private float distace;

    public GameObject bulletPref;
    public float timeBTWshots;
    private float wait;
    public Transform firePoint;
    public float bulletForce;
    public Animator anim;

    public static bool kill;

    void Start()
    {
        mustPatrol = true;
        kill = false;

        playerGO = GameObject.FindGameObjectWithTag("Player");
        if(playerGO != null)
        {
            player = playerGO.GetComponent<Transform>();
        }
    }


    void Update()
    {
        if (mustPatrol)
        {
            mustFlip = !Physics2D.OverlapCircle(groundCheck.position, 0.1f, whatIsGround);
            Patrol();
        }

        

        distace = Vector2.Distance(transform.position, player.position);
        if (distace <= range)
        {
            if(player.position.x > transform.position.x && transform.localScale.x < 0 ||
                player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
            }

            Attack();
        } else
        {
            mustPatrol = true;
        }

    }

    void FixedUpdate()
    {
        if (mustPatrol)
        {
            anim.SetBool("Shoot", false);
        }

        hitA = Physics2D.OverlapCircle(hitPointA.position, 0.1f, whatIsPlayer);
        hitB = Physics2D.OverlapCircle(hitPointB.position, 0.1f, whatIsPlayer);

        if (hitA || hitB)
        {
            if (!PlayerMovement.isDashing)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().Hit();
            }
        }
    }

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

    void Attack()
    {
        mustPatrol = false;
        anim.SetBool("Shoot", true);
        rb.velocity = Vector2.zero;
        wait -= Time.deltaTime;

        if (wait <= 0)
        {
            GameObject newBull = Instantiate(bulletPref, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = newBull.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            wait = timeBTWshots;
        }
    }
}
