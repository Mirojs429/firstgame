using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_Neutral : MonoBehaviour
{
    public Transform start, end;
    public float speed;
    Vector3 nextPos;

    public Transform hitPointA, hitPointB, hitPointC;
    public LayerMask whatIsPlayer;
    public SpriteRenderer render;
    private bool hitA, hitB, hitC;

    public static bool kill;

    void Start()
    {
        nextPos = start.position;
        kill = false;
    }


    void Update()
    {
        if (transform.position == start.position)
        {
            nextPos = end.position;
            render.flipX = true;
        }
        if (transform.position == end.position)
        {
            nextPos = start.position;
            render.flipX = false;
        }
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, nextPos, speed * Time.deltaTime);

        hitA = Physics2D.OverlapCircle(hitPointA.position, 0.1f, whatIsPlayer);
        hitB = Physics2D.OverlapCircle(hitPointB.position, 0.1f, whatIsPlayer);
        hitC = Physics2D.OverlapCircle(hitPointC.position, 0.1f, whatIsPlayer);

        if (hitA || hitB || hitC)
        {
            if (PlayerMovement.isDashing == false)
            {
                kill = true;
            }
        }

    }
}
