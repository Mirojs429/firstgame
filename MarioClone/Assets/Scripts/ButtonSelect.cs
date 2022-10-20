using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<Selectable>().Select();        
    }
}
