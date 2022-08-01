using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float shootingSpace;
    public Transform firePoint;
    public GameObject bulletPref;
    public float bulletForce;

    private float wait;

    void Start()
    {
        
    }

    void Update()
    {
        wait -= Time.deltaTime;

        if (wait <= 0)
        {
            Shoot();
            wait = shootingSpace;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPref, firePoint.transform.position, firePoint.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
    }

}
