using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject brick;
    private LevelDisplay levelDisplay;

    void Awake()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        levelDisplay = gameObject.GetComponent<LevelDisplay>();

        for (int i = 0; i < SaveManager.instance.levels.Length; i++)
        {
            Level lev = (Level)SaveManager.instance.levels[i];
            GameObject b = Instantiate(brick, transform.position, Quaternion.identity);
            levelDisplay = b.GetComponent<LevelDisplay>();
            b.name = lev.levelName;
            b.transform.SetParent(transform);
            levelDisplay.DisplayLevel(lev);
            
        }

    }

}
