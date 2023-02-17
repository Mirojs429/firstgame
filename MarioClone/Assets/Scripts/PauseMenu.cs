using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject deathMenu;
    public bool pause;
    private GameObject player;
    private PlayerHealth playerHealth;
    private PlayerScore playerScore;

    private void Start()
    {
        pause = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerScore = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScore>();
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        PauseGame();
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        ResumeGame();
    }

    public void Restart()
    {
        ResumeGame();
        playerScore.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
    }

    public void MainMenu()
    {
        ResumeGame();
        playerScore.ResetScore();
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7)) && !pause && !playerHealth.PlayerDied())
        {
            Pause();
        }else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7)) && pause && !playerHealth.PlayerDied())
        {
            Resume();
        }

        /*if (pause)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }*/
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; 
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody2D>().gravityScale = 0f;
        player.GetComponent<PlayerMovement>().SetMovement(false);
        pause = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        player.GetComponent<PlayerMovement>().SetMovement(true);
        player.GetComponent<Rigidbody2D>().gravityScale = 3f;
        pause = false;
    }

    public void DeathPause()
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody2D>().gravityScale = 0f;
        pause = true;
    }

    public bool isPaused()
    {
        return pause;
    }
}
