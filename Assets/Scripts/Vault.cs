using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vault : Obstacle
{    
    [SerializeField] private bool turnedOn;


    private void FixedUpdate()
    {
        if(!GameManager.Instance.softPause)
        {
            if (activate && !turnedOn)
            {
                turnedOn = true;
                myAnim.SetTrigger("Open");
            }
            else if (!activate && turnedOn)
            {
                turnedOn = false;
                myAnim.SetTrigger("Close");
            }
        }
    }
}
