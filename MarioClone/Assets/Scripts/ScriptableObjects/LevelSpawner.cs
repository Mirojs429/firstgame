using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] levels;
    [SerializeField] private GameObject brick;
    private LevelDisplay levelDisplay;

    void Start()
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
            b.transform.SetParent(transform);
            levelDisplay.DisplayLevel((Level)levels[i]);
            
        }

    }

}
