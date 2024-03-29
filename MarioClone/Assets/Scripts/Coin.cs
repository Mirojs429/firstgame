using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private int colis = 0;
    
    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("Player"))
        {
            colis += 1;
            if(colis == 1)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScore>().SetCoinsCount(1);
                Destroy(gameObject);
            }
        }

    }
}
