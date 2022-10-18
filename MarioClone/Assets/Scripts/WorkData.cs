using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorkData : MonoBehaviour
{
    public TMP_Text text;

    private float current;
    private int numOfEnemies;
    private int numOfCoins;
    public int targetFrameRate = 60;

    void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = targetFrameRate;
    }

    void Start()
    {
        numOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        numOfCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
    }

    
    void Update()
    {
        current = (int)(1f / Time.unscaledDeltaTime);

        text.text = "---- Work data ----" + "\n\n" + "Colectibles on map" + "\n" +
            "Enemies: " + numOfEnemies.ToString() + "\n" +
            "Coins: " + numOfCoins.ToString() + "\n\n" +
            "FPS: " + current;

        if (Application.targetFrameRate != targetFrameRate)
            Application.targetFrameRate = targetFrameRate;
    }
}
