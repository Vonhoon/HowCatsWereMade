using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBlock : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("EffectBlock") || other.CompareTag("ObstacleBlock"))
        {
            player.GetComponent<Cats>().GiveOrders();
        }
    }
}
