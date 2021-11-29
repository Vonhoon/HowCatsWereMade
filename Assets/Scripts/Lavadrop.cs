using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lavadrop : MonoBehaviour
{
    public bool changed;
    private void OnCollisionEnter2D(Collision2D other)
    {        
        if(other.gameObject.CompareTag("Water"))
        {
            if(!changed)
            {
                gameObject.tag = "Wall";
                GetComponent<SpriteRenderer>().color = Color.gray;
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<Rigidbody2D>().isKinematic = true;
                GetComponent<CircleCollider2D>().enabled = false;
                other.gameObject.SetActive(false);
                changed = true;
            }
        }
    }
}
