using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject deathMenu;
    public bool death = false;

    public void DeathMenu()
    {
        deathMenu.SetActive(true);
    }
}
