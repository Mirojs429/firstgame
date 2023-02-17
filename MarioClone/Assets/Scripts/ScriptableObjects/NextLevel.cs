using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NextLevel : MonoBehaviour
{
    private int colis = 0;
    public GameObject nextLevelMenu;
    [HideInInspector] public int levelID;
    public TMP_Text coins;
    public TMP_Text enemies;
    private PauseMenu pauseMenu;

    private void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            colis += 1;
            if (colis == 1)
            {
                pauseMenu.PauseGame();
                FindObjectOfType<NextLevelLoader>().NextLevelMenuActive();
            }
        }
    }
}
