using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyano : Player
{
    public Animator myAnim;

    public void Update()
    {
        if(!GameManager.Instance.softPause)
        {
            if (move)
            {
                myAnim.SetBool("Move", true);
            }
            else
            {
                myAnim.SetBool("Move", false);
            }
        }
    }

    public override void FixedUpdate()
    {
        if (!GameManager.Instance.softPause)
        {
            if (move)
            {
                if (myDirection == direction.right)
                {
                    transform.Translate(Vector2.right * Time.deltaTime * speed);
                }
                if (myDirection == direction.left)
                {
                    transform.Translate(Vector2.left * Time.deltaTime * speed);
                }
                if (myDirection == direction.up)
                {
                    transform.Translate(Vector2.up * Time.deltaTime * speed);
                }
                if (myDirection == direction.down)
                {
                    transform.Translate(Vector2.down * Time.deltaTime * speed);
                }
            }
        }
    }
}
