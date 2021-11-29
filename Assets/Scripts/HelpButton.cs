using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButton : MonoBehaviour
{

    public List<GameObject> canvasObjects;
    public List<PlayerBlock> playerObjects;    
    public List<ObstacleBlock> obstacleObjects;
    public List<GameObject> objectives;
    public List<GameObject> crown;

    public void ShowHelp()
    {
        Time.timeScale = 0;
        for(int i = 0; i < canvasObjects.Count; i++)
        {
            canvasObjects[i].SetActive(true);
        }
        for (int i = 0; i < playerObjects.Count; i++)
        {
            playerObjects[i].HelpOn(true);
        }
        for (int i = 0; i < obstacleObjects.Count; i++)
        {
            obstacleObjects[i].HelpOn(true);
        }
        for (int i = 0; i < objectives.Count; i++)
        {
            if(objectives[i].activeSelf)
            {
                crown[i].SetActive(true);
                crown[i].transform.position = objectives[i].transform.position;
            }
        }
    }

    public void CloseHelp()
    {
        Time.timeScale = 1;
        for (int i = 0; i < canvasObjects.Count; i++)
        {
            canvasObjects[i].SetActive(false);
        }
        for (int i = 0; i < playerObjects.Count; i++)
        {
            playerObjects[i].HelpOn(false);
        }
        for (int i = 0; i < obstacleObjects.Count; i++)
        {
            obstacleObjects[i].HelpOn(false);
        }
        for (int i = 0; i < objectives.Count; i++)
        {
            if(objectives[i].activeSelf)
            {
                crown[i].SetActive(false);
            }
        }
    }
}
