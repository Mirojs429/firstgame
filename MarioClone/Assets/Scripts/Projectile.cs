using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject destroy;

    void Update()
    {
        Destroy(gameObject, 6f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player")
            || col.gameObject.CompareTag("Terrain")
            || col.gameObject.CompareTag("Enemy")
            || col.gameObject.CompareTag("Trap")
            || col.gameObject.CompareTag("StickyTerrain")
            || col.gameObject.CompareTag("BulletStop"))
        {
            Instantiate(destroy, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
