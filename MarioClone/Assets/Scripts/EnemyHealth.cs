using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Transform deadCheck;
    public LayerMask whatIsPlayer;
    public float silaOdraz;
    public GameObject dead;

    private Rigidbody2D rb;
    private GameObject playerGO;
    private Transform player;


    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        playerGO = GameObject.FindGameObjectWithTag("Player");
        if (playerGO != null)
        {
            player = playerGO.GetComponent<Transform>();
        }
    }

    void Update()
    {
        if(Physics2D.OverlapCircle(deadCheck.position, 0.2f, whatIsPlayer))
        {
            rb = player.transform.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x, silaOdraz);
            Instantiate(dead, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }

}
