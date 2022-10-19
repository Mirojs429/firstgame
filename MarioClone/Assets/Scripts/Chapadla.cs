using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapadla : MonoBehaviour
{
    public Animator anim;
    //public float waitDown;
    public float waitUp;
    public float offset;
    public int[] range;
    private int random;
    private float waitTime;
    private bool up = false;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        if (offset == 0)
        {
            waitTime = 0;
        }
        else
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
                anim.Play("ChapadlaDown");
                random = Random.Range(range[0], range[1]);
                waitTime = random;
                up = false;
            }
        }
        else
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                anim.Play("ChapadlaUp");
                waitTime = waitUp;
                up = true;
            }
        }
    }
}
