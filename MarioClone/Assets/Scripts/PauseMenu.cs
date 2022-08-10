using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject deathMenu;
    public static bool pause = false;

    private void Start()
    {

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
        Time.timeScale = 1f;
        pause = false;
        PlayerScore.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
    }

    public void DeathMenu()
    {
        Debug.Log("Mrtev");
        deathMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pause)
        {
            Pause();
        }else if (Input.GetKeyDown(KeyCode.Escape) && pause)
        {
            Resume();
        }
    }
}
