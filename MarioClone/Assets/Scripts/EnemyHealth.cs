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
        if (PlayerMovement.isDashing == false)
        {
            if (Physics2D.OverlapBox(deadCheck.position, new Vector2(.4f, .15f), 0, whatIsPlayer))
            {
                player.GetComponent<PlayerScore>().SetEnemiesCount(1);
                rb = player.transform.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(rb.velocity.x, silaOdraz);
                Instantiate(dead, transform.position, Quaternion.identity);
                gameObject.SetActive(false);
            }
        }        
    }

}
