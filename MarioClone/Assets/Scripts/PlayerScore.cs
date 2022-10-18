using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    [Header("Coins")]
    public static int coins = 0;
    public TMP_Text coinsScore;

    [Header("Enemies")]
    public static int enemies = 0;
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

    public static void ResetScore()
    {
        coins = 0;
        enemies = 0;
    }
}
