using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public static int coins = 0;
    public TMP_Text coinsScore;
    void Start()
    {
        coinsScore.text = "Coins: " + coins.ToString();
    }

    void Update()
    {
        coinsScore.text = "Coins: " + coins.ToString();
    }

    public static void ResetScore()
    {
        coins = 0;
    }
}
