using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform start, end;
    public float speed;
    Vector3 nextPos;

    void Start()
    {
        nextPos = start.position;
    }


    void Update()
    {
        if (transform.position == start.position)
        {
            nextPos = end.position;
        }
        if (transform.position == end.position)
        {
            nextPos = start.position;
        }
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, nextPos, speed * Time.deltaTime);

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.transform.SetParent(null);
        }
    }
}
