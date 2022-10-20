using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu, levelSelectorMenu, areYouSure, settings, credits;
    public GameObject newGame, continueB, levelSelector, settingsBack, creditsBack;

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
        credits.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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

    public void Settings()
    {
        settings.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void SettingsBack()
    {
        settings.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void Credits()
    {
        credits.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CloseCredits()
    {
        credits.SetActive(false);
        mainMenu.SetActive(true);
    }
}
