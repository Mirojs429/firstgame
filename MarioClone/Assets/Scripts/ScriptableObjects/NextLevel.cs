using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] private ScriptableObject level;
    [SerializeField] private ScriptableObject nextLevel;
    private int colis = 0;

    public void ChangeColectredCoins(Level _level)
    {
        _level.colectedCoins = PlayerScore.coins;
    }

    public void LoadLevel(Level _nextLevel)
    {
        _nextLevel.levelLock = false;
        SceneManager.LoadScene(_nextLevel.sceneToLoad.name);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            colis += 1;
            if (colis == 1)
            {
                ChangeColectredCoins((Level)level);
                LoadLevel((Level)nextLevel);
            }
        }
    }
}
