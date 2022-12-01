using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject deathMenu;
    public static bool pause;

    private void Start()
    {
        pause = false;
        deathMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        pause = true;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        pause = false;
    }

    public void Restart()
    {
        pause = false;
        Time.timeScale = 1f;
        PlayerScore.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        PlayerScore.ResetScore();
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7)) && !pause && !PlayerHealth.died)
        {
            Pause();
        }else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7)) && pause && !PlayerHealth.died)
        {
            Resume();
        }

        if (pause)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }


}
