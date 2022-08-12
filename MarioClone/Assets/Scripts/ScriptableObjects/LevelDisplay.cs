using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text levelName;
    [SerializeField] private TMP_Text levelID;
    [SerializeField] private TMP_Text coins;
    [SerializeField] private Image levelIMG;
    [SerializeField] private Image lockIMG;
    [SerializeField] private Button levelButton;
    private bool levelLock;


    public void DisplayLevel(Level _level) 
    {
        levelName.text = _level.levelName;
        levelID.text = _level.levelID.ToString();
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
        if (levelLock)
        {
            levelIMG.color = Color.gray;
        } else
        {
            levelIMG.color = Color.white;
        }


        levelButton.onClick.RemoveAllListeners();
        levelButton.onClick.AddListener(() => SceneManager.LoadScene(_level.sceneToLoad.name));
    }

    public void ChangeLock(Level _level)
    {
        levelLock = false;
    }
}
