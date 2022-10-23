using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject credits;
    private int levelIndex;
    private Level currentLevel;

    public void SaveLevel()
    {
        if (SaveManager.instance.coinsInLevel[SceneManager.GetActiveScene().buildIndex] < PlayerScore.coins)
        {
            SaveManager.instance.coinsInLevel[SceneManager.GetActiveScene().buildIndex] = PlayerScore.coins;
        }

        SaveManager.instance.Save();
        PlayerScore.ResetScore();
    }

    public void Start()
    {
        credits.SetActive(false);
        levelIndex = SceneManager.GetActiveScene().buildIndex;
        currentLevel = (Level)SaveManager.instance.levels[levelIndex - 1];
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            credits.SetActive(true);
            SaveLevel();
        }
    }
}
