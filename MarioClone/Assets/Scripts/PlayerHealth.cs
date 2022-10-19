using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public GameObject deathMenu;
    public Animator anim;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (gameObject.transform.position.y <= -100)
        {
            PauseMenu.pause = true;
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
            anim.Play("Player_death");
        }

        if(Enemy.kill == true || FireEnemy.kill == true || Bat_Neutral.kill == true)
        {
            PauseMenu.pause = true;
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
            anim.Play("Player_death");
        }
    }

    public void Death()
    {
        deathMenu.SetActive(true);
        Time.timeScale = 0f;
        PauseMenu.pause = true;
        Enemy.kill = false;
        FireEnemy.kill = false;

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (PlayerMovement.isDashing == false)
        {
            if (col.gameObject.CompareTag("Trap"))
            {
                PauseMenu.pause = true;
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;
                anim.Play("Player_death");
            }
        }
    }

}
