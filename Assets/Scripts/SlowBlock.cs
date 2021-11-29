using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBlock : MonoBehaviour
{
    private LineRenderer line;
    [SerializeField] private List<GameObject> connected = new List<GameObject>();
    public ParticleSystem particle;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        particle.Clear();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("PlayerBlock"))
        {
            if (!GetComponent<DragScript>().dragging && !other.GetComponent<DragScript>().dragging)
            {
                other.GetComponent<PlayerBlock>().player.GetComponent<Player>().speed = 0.5f;
                if (!connected.Contains(other.gameObject))
                {
                    connected.Add(other.gameObject);
                }
                particle.Play();
                EnableLine();
            }
            if (other.GetComponent<DragScript>().dragging)
            {
                if (connected.Contains(other.gameObject))
                {
                    connected.RemoveAt(connected.IndexOf(other.gameObject));
                }
                other.GetComponent<PlayerBlock>().player.GetComponent<Player>().speed = 1f;
                EnableLine();
            }
            if (GetComponent<DragScript>().dragging)
            {
                other.GetComponent<PlayerBlock>().player.GetComponent<Player>().speed = 1f;
                connected.Clear();
                line.positionCount = 0;
                line.enabled = false;
            }
        }
        if(other.CompareTag("ObstacleBlock"))
        {
            if (!GetComponent<DragScript>().dragging && !other.GetComponent<DragScript>().dragging)
            {
                for (int i = 0; i < other.GetComponent<ObstacleBlock>().obstacleList.Count; i++)
                {
                    if (other.GetComponent<ObstacleBlock>().obstacleList[i].GetComponent<Obstacle>().isMovable)
                    {
                        other.GetComponent<ObstacleBlock>().obstacleList[i].GetComponent<Obstacle>().speed = 0.5f;
                    }
                }             
                if (!connected.Contains(other.gameObject))
                {
                    connected.Add(other.gameObject);
                }
                particle.Play();
                EnableLine();
            }
            if (other.GetComponent<DragScript>().dragging)
            {
                if (connected.Contains(other.gameObject))
                {
                    connected.RemoveAt(connected.IndexOf(other.gameObject));
                }

                for (int i = 0; i < other.GetComponent<ObstacleBlock>().obstacleList.Count; i++)
                {
                    if (other.GetComponent<ObstacleBlock>().obstacleList[i].GetComponent<Obstacle>().isMovable)
                    {
                        other.GetComponent<ObstacleBlock>().obstacleList[i].GetComponent<Obstacle>().speed = 1f;
                    }
                }             
                 EnableLine();
            }
            if (GetComponent<DragScript>().dragging)
            {
                for (int i = 0; i < other.GetComponent<ObstacleBlock>().obstacleList.Count; i++)
                {
                    if (other.GetComponent<ObstacleBlock>().obstacleList[i].GetComponent<Obstacle>().isMovable)
                    {
                        other.GetComponent<ObstacleBlock>().obstacleList[i].GetComponent<Obstacle>().speed = 1f;
                    }
                }             
                connected.Clear();
                line.positionCount = 0;
                line.enabled = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("PlayerBlock"))
        {
            other.GetComponent<PlayerBlock>().player.GetComponent<Player>().speed = 1f;
            if (connected.Contains(other.gameObject))
            {
                connected.RemoveAt(connected.IndexOf(other.gameObject));
            }
            EnableLine();
        }
        if(other.CompareTag("ObstacleBlock"))
        {
            for (int i = 0; i < other.GetComponent<ObstacleBlock>().obstacleList.Count; i++)
            {
                if (other.GetComponent<ObstacleBlock>().obstacleList[i].GetComponent<Obstacle>().isMovable)
                {
                    other.GetComponent<ObstacleBlock>().obstacleList[i].GetComponent<Obstacle>().speed = 1f;
                }
            }  
            if (connected.Contains(other.gameObject))
            {
                connected.RemoveAt(connected.IndexOf(other.gameObject));
            }
            EnableLine();
        }
    }

    private void EnableLine()
    {
        if (connected.Count > 0)
        {
            line.positionCount = connected.Count * 2;
            for (int i = 0; i < connected.Count; i++)
            {
                line.SetPosition(i * 2, transform.position);
                line.SetPosition((i * 2) + 1, connected[i].transform.position);
            }
            line.enabled = true;
        }
        else
        {
            line.enabled = false;
        }
    }
}
