using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderTop : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime;
    
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if(Input.GetAxis("Vertical") == 0)
        {
            effector.rotationalOffset = 0f;
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            effector.rotationalOffset = 0f;
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            effector.rotationalOffset = 180f;
        }
    }
}
