using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private void Update()
    {
        if (gameObject.transform.position.y <= -100)
        {
                Death();
        }
    }

    public static void Death()
    {
        PlayerScore.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Trap"))
        {
            Death();
        }
    }

}
