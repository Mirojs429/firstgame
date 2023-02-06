using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGChanger : MonoBehaviour
{
    public GameObject bg1, bg2, bg3;
    void Start()
    {
        bg1.SetActive(true);
        bg2.SetActive(false);
        bg3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            bg1.SetActive(true);
            bg2.SetActive(false);
            bg3.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            bg1.SetActive(false);
            bg2.SetActive(true);
            bg3.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            bg1.SetActive(false);
            bg2.SetActive(false);
            bg3.SetActive(true);
        }
    }
}
