using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    [Header("Coins")]
    public int coins = 0;
    public TMP_Text coinsScore;

    [Header("Enemies")]
    public int enemies = 0;
    public TMP_Text enemiesScore;

    void Start()
    {        
        coinsScore.text = coins.ToString();
        enemiesScore.text = enemies.ToString();
    }

    void Update()
    {
        coinsScore.text = coins.ToString();
        enemiesScore.text = enemies.ToString();
    }

    public void ResetScore()
    {
        coins = 0;
        enemies = 0;
    }

    public void SetEnemiesCount(int i)
    {
        enemies += i;
    }

    public void SetCoinsCount(int i)
    {
        coins += i;
    }

    public int GetCoinsCount()
    {
        return coins;
    }

    public int GetEnemiesCount()
    {
        return enemies;
    }
}
