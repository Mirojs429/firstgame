using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public static int coins = 0;
    public TMP_Text coinsScore;
    void Start()
    {        
        coinsScore.text = coins.ToString();
    }

    void Update()
    {
        coinsScore.text = coins.ToString();        
    }

    public static void ResetScore()
    {
        coins = 0;
    }
}
