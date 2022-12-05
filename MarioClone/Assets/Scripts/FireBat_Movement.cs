using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBat_Movement : MonoBehaviour
{
    public Transform start, end;
    public float speed;
    Vector3 nextPos;
    public Rigidbody2D rb;
    public GameObject bulletPref;
    public float bulletForce;
    public Transform firePoint;
    public float timeBTWshots;

    public Transform hitPointA, hitPointB, hitPointC;
    public LayerMask whatIsPlayer;
    public SpriteRenderer render;
    private bool hitA, hitB, hitC;
    private GameObject player;
    public float range;

    private float distance;
    private float gunAngle;
    private float wait;

    public static bool kill;

    void Start()
    {
        nextPos = start.position;
        kill = false;
        player = GameObject.Find("Player");
    }


    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= range)
        {
            Attack();
        } else if (distance > range)
        {
            Patrol();
        }

        hitA = Physics2D.OverlapCircle(hitPointA.position, 0.1f, whatIsPlayer);
        hitB = Physics2D.OverlapCircle(hitPointB.position, 0.1f, whatIsPlayer);
        hitC = Physics2D.OverlapCircle(hitPointC.position, 0.1f, whatIsPlayer);

        if (hitA || hitB || hitC)
        {
            if (PlayerMovement.isDashing == false)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().Hit();
            }
        }

        Vector2 playerPose = player.transform.position;
        Vector2 firePos = transform.position;
        playerPose.x = playerPose.x - firePos.x;
        playerPose.y = playerPose.y - firePos.y;
        gunAngle = Mathf.Atan2(playerPose.y, playerPose.x) * Mathf.Rad2Deg - 90f;
        firePoint.rotation = Quaternion.Euler(new Vector3(0f, 0f, gunAngle));

    }

    private void Attack()
    {
        rb.velocity = Vector2.zero;
        wait -= Time.deltaTime;

        if (wait <= 0)
        {
            GameObject newBull = Instantiate(bulletPref, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = newBull.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            wait = timeBTWshots;
        }
    }

    void Patrol()
    {
        rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        speed *= -1;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col != null)
        {
            if (col.name == "End")
            {
                Flip();
            }
            else if (col.name == "Start")
            {
                Flip();
            }
        }
    }
}
