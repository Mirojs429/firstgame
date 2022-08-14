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
    public BoxCollider2D col,triger;
    public SpriteRenderer ren;
    public bool canRespawn;
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
        if (canRespawn)
        {
            if (res)
            {
                if (oldTime + respawnTime <= Time.time)
                {
                    ren.enabled = true;
                    col.enabled = true;
                    triger.enabled = true;
                    res = false;
                    anim.Play("Respawn");
                }
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
        triger.enabled = false;
    }

    /*private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            oldTime = Time.time;
            con = true;
        }
    }*/

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            oldTime = Time.time;
            con = true;
        }
    }
}
