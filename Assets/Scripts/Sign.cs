using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    private enum direction { right, left, up, down}
    [SerializeField] private direction myDir;

    private void OnTriggerStay2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Predator")) && other.gameObject.GetComponent<Player>().hasSight)
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
        else if (other.gameObject.CompareTag("Obstacle") && other.gameObject.GetComponent<Obstacle>().hasSight)
        {
            if (myDir == direction.right)
            {               
                other.gameObject.transform.localEulerAngles = new Vector3(0, 0, 90);
            }
            if (myDir == direction.left)
            {             
                other.gameObject.transform.localEulerAngles = new Vector3(0, 0, -90);
            }
            if (myDir == direction.up)
            {             
                other.gameObject.transform.localEulerAngles = new Vector3(0, 0, 180);
            }
            if (myDir == direction.down)
            {             
                other.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
        }
    } 
}
