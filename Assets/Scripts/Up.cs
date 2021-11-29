using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up : MonoBehaviour
{
    public bool activate;
    //public float power;
    //private Animator myAnim;

    private void Start()
    {
        //myAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if((other.tag == "Player" || other.tag == "Enemy") && activate)
        {
            Debug.Log("move up");
            other.GetComponent<Player>().myDirection = Player.direction.up;
            other.GetComponent<Player>().rb.gravityScale = 0;
            //StartCoroutine(Activate());
        }
    }
}
