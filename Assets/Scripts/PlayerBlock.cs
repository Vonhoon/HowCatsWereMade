using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    public GameObject player;
    private LineRenderer line;    

    private void Start()
    {
        line = GetComponent<LineRenderer>();    
    }

    public void HelpOn(bool on)
    {
        if(on)
        {
            line.positionCount = 2;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, player.transform.position);
            line.enabled = true;
        }
        else
        {
            line.enabled = false;
        }
    }
}
