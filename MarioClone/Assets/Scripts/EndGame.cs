using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject credits;

    [HideInInspector] public int levelID;
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

    public void Start()
    {
        credits.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            //PuseMenu.pause = true;
            credits.SetActive(true);
            levelID = SceneManager.GetActiveScene().buildIndex;
            SaveLevel();
        }
    }
}
