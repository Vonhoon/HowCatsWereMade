using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaArea : MonoBehaviour
{

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Predator"))
        {
            if (other.gameObject.GetComponent<Player>().noGrav == true)
            {
                other.gameObject.GetComponent<Player>().noGrav = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Predator"))
        {
            if(other.gameObject.GetComponent<Player>().noGrav == false)
            {
                other.gameObject.GetComponent<Player>().noGrav = true;
            }
        }
    }
}
