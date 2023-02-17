using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SginMenu : MonoBehaviour
{
    public GameObject box;
    public TMP_Text msg;
    private bool showed = false;
    private PauseMenu pauseMenu;

    private void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && showed)
        {
            pauseMenu.ResumeGame();
            box.transform.localScale = new Vector3(0,0,0);
            box.SetActive(false);
            showed = false;
        }
    }

    public void WriteMsg(string txt)
    {
        box.SetActive(true);
        LeanTween.scale(box, new Vector3(1f, 1f, 1f), .3f).setIgnoreTimeScale(true).setEase(LeanTweenType.easeInQuad);
        msg.SetText(txt);
        showed = true;
        pauseMenu.PauseGame();
    }
}
