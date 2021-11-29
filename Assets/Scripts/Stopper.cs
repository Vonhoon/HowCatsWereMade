using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopper : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
