using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float lenght, startPos;
    public GameObject cam;
    public float parallaxEffect;

    void Start()
    {
        startPos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float dis = (cam.transform.position.x * parallaxEffect);
        float temp = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startPos + dis, transform.position.y, transform.position.z);

        if (temp > startPos + lenght)
        {
            startPos += lenght;
        } else if (temp < startPos) {
            startPos -= lenght;
        }
    }
}
