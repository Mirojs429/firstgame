using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public Animator anim;
    public float waitDown;
    public float waitUp;
    public float offset;

    private float waitTime;
    private bool up = true;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        if (offset == 0)
        {
            waitTime = 0;
        } else
        {
            waitTime = offset;
        }
    }

    
    void Update()
    {
        if (up)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                anim.Play("SpikeDown");
                waitTime = waitDown;
                up = false;
            }
        } else
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                anim.Play("SpikeUp");
                waitTime = waitUp;
                up = true;
            }
        }
    }
}
