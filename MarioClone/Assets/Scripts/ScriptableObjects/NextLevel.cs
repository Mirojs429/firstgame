using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private ScriptableObject level;
    [SerializeField] private ScriptableObject nextLevel;
    private int colis = 0;

    public void LoadLevel()
    {
        if (SaveManager.instance.coinsInLevel[SceneManager.GetActiveScene().buildIndex] < PlayerScore.coins)
        {
            SaveManager.instance.coinsInLevel[SceneManager.GetActiveScene().buildIndex] = PlayerScore.coins;
        }
        
        PlayerScore.ResetScore();
        int levelID = SceneManager.GetActiveScene().buildIndex + 1;
        
        if (SceneUtility.GetScenePathByBuildIndex(levelID) != "")
        {            
            SaveManager.instance.levelLock[levelID] = 1;
            SaveManager.instance.Save();            
            SceneManager.LoadScene(levelID);
        } else {
            SceneManager.LoadScene(0);
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
                LoadLevel();
            }
        }
    }
}
