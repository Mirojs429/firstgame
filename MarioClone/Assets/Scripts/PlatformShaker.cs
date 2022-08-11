using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformShaker : MonoBehaviour
{
    public float wait;
    private float oldTime;
    private bool con = false;
    private bool res = false;
    public Animator anim;
    public BoxCollider2D col;
    public SpriteRenderer ren;
    public float respawnTime;

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
                con = false;
                res = true;
                oldTime = Time.time;
            }
        }

        if (res)
        {
            if (oldTime + respawnTime <= Time.time)
            {
                ren.enabled = true;
                col.enabled = true;
                res = false;
                anim.Play("Respawn");
            }
        }
        
    }

    public void ShakeDelete()
    {
        ren.enabled = false;
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
