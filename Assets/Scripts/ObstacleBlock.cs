using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBlock : MonoBehaviour
{
    //public GameObject obstacle;
    public List<GameObject> obstacleList = new List<GameObject>();
    private LineRenderer line;
   

    private void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    public void HelpOn(bool on)
    {
        if (on)
        {
            for(int i = 0; i < obstacleList.Count; i++)
            {
                line.positionCount = 2 * (i + 1);
                line.SetPosition(2 * i, transform.position);
                line.SetPosition((2 * i) + 1, obstacleList[i].transform.position);
            }            
            line.enabled = true;
        }
        else
        {
            line.enabled = false;
        }
    }
}
