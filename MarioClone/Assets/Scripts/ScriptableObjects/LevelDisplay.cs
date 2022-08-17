using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text levelName;
    [SerializeField] private TMP_Text coins;
    [SerializeField] private Image levelIMG;
    [SerializeField] private Image lockIMGalpha;
    [SerializeField] private Image lockIMG;
    [SerializeField] private Button levelButton;
    private bool levelLock;
    private int levelIndex;


    public void DisplayLevel(Level _level) 
    {
        levelName.text = _level.levelName;
        levelIMG.sprite = _level.levelIMG;

        if (SaveManager.instance.levelLock[_level.levelID] == 1)
        {
            levelLock = false;
        } else
        {
            levelLock = true;
        }

        coins.text = SaveManager.instance.coinsInLevel[_level.levelID].ToString() + " / " + _level.maxCoins.ToString();

        levelButton.interactable = !levelLock;
        lockIMG.enabled = levelLock;
        lockIMGalpha.color = Color.black;
        var tempColor = lockIMGalpha.color;
        if (levelLock)
        {
            tempColor.a = .25f;
        } else
        {
            tempColor.a = 0f;
        }
        lockIMGalpha.color = tempColor;

        levelIndex = _level.levelID;
    }

    public void ChangeLock(Level _level)
    {
        levelLock = false;
    }

    public void PlayLevel()
    {
        SceneManager.LoadScene(levelIndex);
    }
}
