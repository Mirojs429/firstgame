using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
        // transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, offset.z);
    }
}
