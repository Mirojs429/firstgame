using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NextLevelLoader : MonoBehaviour
{
    public GameObject nextLevelMenu;
    [HideInInspector] public int levelID;
    public TMP_Text coins;
    public TMP_Text enemies;
    private Level nextLevel;
    private int numOfEnemies;
    private PlayerScore playerScore;
    private PauseMenu pauseMenu;

    private int levelIndex;
    private Level currentLevel;

    private void Start()
    {
        numOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        levelIndex = SceneManager.GetActiveScene().buildIndex;
        currentLevel = (Level)SaveManager.instance.levels[levelIndex - 1];
        playerScore = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScore>();
        pauseMenu = FindObjectOfType<PauseMenu>();
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
        pauseMenu.ResumeGame();
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
        pauseMenu.ResumeGame();
        playerScore.ResetScore();
        SceneManager.LoadScene(0);
    }

    public void SaveLevel()
    {
        if (SaveManager.instance.coinsInLevel[currentLevel.levelID] < playerScore.GetCoinsCount())
        {
            SaveManager.instance.coinsInLevel[currentLevel.levelID] = playerScore.GetCoinsCount();
        }

        playerScore.ResetScore();

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

    public void NextLevelMenuActive()
    {
        nextLevelMenu.SetActive(true);
        coins.text = playerScore.GetCoinsCount() + " / " + currentLevel.maxCoins.ToString();
        enemies.text = playerScore.GetEnemiesCount() + " / " + numOfEnemies;
        SaveLevel();
    }
}
