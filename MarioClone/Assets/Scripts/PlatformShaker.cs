using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformShaker : MonoBehaviour
{
    public float wait;
    private float oldTime;
    private bool con = false;
    public Animator anim;
    public BoxCollider2D col;

    void Start()
    {
        
    }

    void Update()
    {
        if (con)
        {
            if (oldTime + wait <= Time.time)
            {
                anim.Play("Shake");
            }
        }
        
    }

    public void ShakeDelete()
    {
        gameObject.SetActive(false);
    }

    public void ColliderDelite()
    {
        col.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            oldTime = Time.time;
            con = true;
        }
    }
}
