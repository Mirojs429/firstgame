using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu, levelSelectorMenu, areYouSure;
    public GameObject newGame, continueB, levelSelector;

    private void Awake()
    {
        if (!File.Exists(Application.persistentDataPath + "/gamesave.dat"))
        {
            continueB.SetActive(false);
            levelSelector.SetActive(false);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        if (!File.Exists(Application.persistentDataPath + "/gamesave.dat"))
        {
            SaveManager.instance.Save();
            SceneManager.LoadScene(1);
        } else
        {
            areYouSure.SetActive(true);
            mainMenu.SetActive(false);
        }
        
    }

    public void LevelSelector()
    {
        levelSelectorMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void SelectorBack()
    {
        levelSelectorMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void AreYouSureNObtn()
    {
        areYouSure.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void AreYouSureYESbtn()
    {
        File.Delete(Application.persistentDataPath + "/gamesave.dat");
        SaveManager.instance.Load();
        SaveManager.instance.Save();
        SceneManager.LoadScene(1);
    }

    public void Continue()
    {
        int l = SaveManager.instance.levelLock.Length;
        for (int i = 1; i < l; i++)
        {
            if (SaveManager.instance.levelLock[i] == 0)
            {
                SceneManager.LoadScene(i - 1);
                return;
            } else
            {
                if (i + 1 == l)
                {
                    SceneManager.LoadScene(i);
                }
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
