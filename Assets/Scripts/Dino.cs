using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dino : MonoBehaviour
{
    [SerializeField] private float speed;
    public Transform waypointOne;
    
    private void FixedUpdate()
    {
        if(!GameManager.Instance.softPause)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypointOne.position, Time.deltaTime * speed);
        }        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Lava"))
        {
            Destroy(gameObject);
        }      
    }
}
