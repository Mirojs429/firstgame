using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UILoader : MonoBehaviour
{
    [SerializeField] private bool enableDoubleJump;
    [SerializeField] private bool enableDash;
    private GameObject player;

    private void Awake()
    {
        if (!SceneManager.GetSceneByName("UI").isLoaded)
        {
            SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().EnableDoubleJump(enableDoubleJump);
            player.GetComponent<PlayerMovement>().EnableDash(enableDash);
        }
    }
}
