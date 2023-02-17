using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    private bool signColl = false;
    public GameObject buttonToPress;
    public string msgTxt;
    private SginMenu write;
    private PauseMenu pauseMenu;

    void Start()
    {
        write = FindObjectOfType<SginMenu>();
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && signColl)
        {
            write.WriteMsg(msgTxt);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            buttonToPress.SetActive(true);
            signColl = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            buttonToPress.SetActive(false);
            signColl = false;
        }
    }
}
