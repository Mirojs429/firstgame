using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject deathMenu;
    public Animator anim;
    private Rigidbody2D rb;
    private PauseMenu pauseMenu;
    private bool died;

    private void Start()
    {
        died = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    private void Update()
    {
        if (gameObject.transform.position.y <= -100)
        {
            pauseMenu.DeathPause();
            anim.Play("Player_death");
        }
    }

    public void Death()
    {
        deathMenu.SetActive(true);
        pauseMenu.PauseGame();
        died = true;
    }

    public void Hit()
    {
        pauseMenu.DeathPause();
        anim.Play("Player_death");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (PlayerMovement.isDashing == false)
        {
            if (col.gameObject.CompareTag("Trap"))
            {
                pauseMenu.DeathPause();
                anim.Play("Player_death");
            }
        }
    }

    public bool PlayerDied()
    {
        return died;
    }

}
