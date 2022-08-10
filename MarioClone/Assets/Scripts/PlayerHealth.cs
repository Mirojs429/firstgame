using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public GameObject deathMenu;
    public Animator anim;

    private void Update()
    {
        if (gameObject.transform.position.y <= -100)
        {
                Death();
        }

        if(Enemy.kill == true || FireEnemy.kill == true)
        {
            Death();
        }
    }

    public void Death()
    {
        //PauseMenu.DeathMenu();
        //PlayerScore.ResetScore();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        anim.Play("Character_death");
        deathMenu.SetActive(true);
        Time.timeScale = 0f;
        PauseMenu.pause = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Trap"))
        {
            Death();
        }
    }

}
