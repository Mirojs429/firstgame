using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NextLevelLoader : MonoBehaviour
{
    private int colis = 0;
    public GameObject nextLevelMenu;
    [HideInInspector] public int levelID;
    public TMP_Text coins;
    public TMP_Text enemies;
    private Level nextLevel;
    private int numOfEnemies;

    private int levelIndex;
    private Level currentLevel;

    private void Start()
    {
        numOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        levelIndex = SceneManager.GetActiveScene().buildIndex;
        currentLevel = (Level)SaveManager.instance.levels[levelIndex - 1];
        if ((Level)SaveManager.instance.levels[levelIndex] != null)
        {
            nextLevel = (Level)SaveManager.instance.levels[levelIndex];
        }
        else
        {
            nextLevel = currentLevel;
        }
    }

    public void NextLevelButton()
    {
        Time.timeScale = 1f;
        PauseMenu.pause = false;
        if (SceneUtility.GetScenePathByBuildIndex(nextLevel.levelID) != "")
        {
            SaveManager.instance.levelLock[nextLevel.levelID] = 1;
            SaveManager.instance.Save();
            SceneManager.LoadScene(nextLevel.levelID);
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
        PlayerScore.ResetScore();
        SceneManager.LoadScene(0);
    }

    public void SaveLevel()
    {
        if (SaveManager.instance.coinsInLevel[currentLevel.levelID] < PlayerScore.coins)
        {
            SaveManager.instance.coinsInLevel[currentLevel.levelID] = PlayerScore.coins;
        }

        PlayerScore.ResetScore();

        if (SceneUtility.GetScenePathByBuildIndex(currentLevel.levelID) != "")
        {
            SaveManager.instance.levelLock[nextLevel.levelID] = 1;
            SaveManager.instance.Save();
        }
        else
        {
            SaveManager.instance.Save();
        }
    }

    public void NextLevelActive()
    {
        nextLevelMenu.SetActive(true);
        coins.text = PlayerScore.coins + " / " + currentLevel.maxCoins.ToString();
        enemies.text = PlayerScore.enemies.ToString() + " / " + numOfEnemies;
        SaveLevel();
    }
}
