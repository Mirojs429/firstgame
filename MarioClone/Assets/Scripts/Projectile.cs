using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player")
            || col.gameObject.CompareTag("Terrain")
            || col.gameObject.CompareTag("BulletStop"))
        {
            Destroy(gameObject);
        }
    }
}
