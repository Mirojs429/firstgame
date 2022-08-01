using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private float input;
    private float wait;

    public float waitTime;

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        input = Input.GetAxisRaw("Vertical");

        if (input == 0)
        {
            wait = waitTime;
        }

        if (input < 0 || Input.GetButtonDown("Jump"))
        {
            if (wait <= 0)
            {
                effector.rotationalOffset = 180f;
                wait = waitTime;
            } else
            {
                wait -= Time.deltaTime;
            }
        }

        if (input > 0 || Input.GetButtonDown("Jump"))
        {
            effector.rotationalOffset = 0f;
        }
    }
}
