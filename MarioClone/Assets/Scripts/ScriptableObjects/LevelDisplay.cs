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


    public void DisplayLevel(Level _level) 
    {
        levelName.text = _level.levelName;
        levelID.text = _level.levelID.ToString();
        levelIMG.sprite = _level.levelIMG;
        coins.text = _level.colectedCoins + " / " + _level.maxCoins;
        
        levelButton.interactable = !_level.levelLock;
        lockIMG.enabled = _level.levelLock;
        if (_level.levelLock)
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
        _level.levelLock = false;
    }
}
