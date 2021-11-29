using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPad : MonoBehaviour
{
    public GameObject door;
    public enum direction { right, left, up, down }
    public direction myDir;

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            GetComponent<Animator>().SetBool("Activate", true);
            door.GetComponent<Animator>().SetBool("Activate", true);
            if(other.gameObject.GetComponent<Player>().hasSight)
            {
                if (myDir == direction.right)
                {
                    other.gameObject.GetComponent<Player>().myDirection = Player.direction.right;
                }
                if (myDir == direction.left)
                {
                    other.gameObject.GetComponent<Player>().myDirection = Player.direction.left;
                }
                if (myDir == direction.up)
                {
                    other.gameObject.GetComponent<Player>().myDirection = Player.direction.up;
                }
                if (myDir == direction.down)
                {
                    other.gameObject.GetComponent<Player>().myDirection = Player.direction.down;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
        {
            GetComponent<Animator>().SetBool("Activate", false);
            door.GetComponent<Animator>().SetBool("Activate", false);
        }
    }
}
