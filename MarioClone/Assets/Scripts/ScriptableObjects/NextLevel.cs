using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NextLevel : MonoBehaviour
{
    private int colis = 0;
    public GameObject nextLevelMenu;
    [HideInInspector] public int levelID;
    public TMP_Text coins;
    public Level level;

    public void SaveLevel()
    {
        if (SaveManager.instance.coinsInLevel[SceneManager.GetActiveScene().buildIndex] < PlayerScore.coins)
        {
            SaveManager.instance.coinsInLevel[SceneManager.GetActiveScene().buildIndex] = PlayerScore.coins;
        }
        
        PlayerScore.ResetScore();

        if (SceneUtility.GetScenePathByBuildIndex(levelID) != "")
        {
            SaveManager.instance.levelLock[levelID] = 1;
            SaveManager.instance.Save();
        }
        else
        {
            SaveManager.instance.Save();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            colis += 1;
            if (colis == 1)
            {
                Time.timeScale = 0f;
                PauseMenu.pause = true;
                levelID = SceneManager.GetActiveScene().buildIndex + 1;
                nextLevelMenu.SetActive(true);
                coins.text = PlayerScore.coins + " / " + level.maxCoins.ToString();
                SaveLevel();
            }
        }
    }

    public void NextLevelButton()
    {
        Time.timeScale = 1f;
        PauseMenu.pause = false;
        if (SceneUtility.GetScenePathByBuildIndex(levelID) != "")
        {
            SaveManager.instance.levelLock[levelID] = 1;
            SaveManager.instance.Save();
            SceneManager.LoadScene(levelID);
        }
        else
        {
            SceneManager.LoadScene(0);
            SaveManager.instance.Save();
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        PauseMenu.pause = false;
        PlayerScore.coins = 0;
        SceneManager.LoadScene(0);
    }
}
