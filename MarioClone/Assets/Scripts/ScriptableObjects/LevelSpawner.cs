using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private Level[] levels;
    [SerializeField] private GameObject brick;
    private LevelDisplay levelDisplay;

    void Awake()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        levelDisplay = gameObject.GetComponent<LevelDisplay>();

        for (int i = 0; i < levels.Length; i++)
        {
            GameObject b = Instantiate(brick, transform.position, Quaternion.identity);
            levelDisplay = b.GetComponent<LevelDisplay>();
            b.name = levels[i].levelName;
            b.transform.SetParent(transform);
            levelDisplay.DisplayLevel(levels[i]);
            
        }

    }

}
