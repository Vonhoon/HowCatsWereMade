using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    private LineRenderer line;
    [SerializeField] private List<GameObject> connected = new List<GameObject>();
    public ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        particle.Clear();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBlock")) //태그로 블록타입 구분
        {          
            if (!other.GetComponent<DragScript>().dragging && !GetComponent<DragScript>().dragging) //클릭 중이 아닐때만 실행
            {
                other.GetComponent<PlayerBlock>().player.GetComponent<Player>().move = true;
                if (!connected.Contains(other.gameObject)) // 선 긋기용 연결된 블록 리스트에 담기.
                {
                    connected.Add(other.gameObject);
                }
                particle.Play();
                EnableLine(); // 블록 리스트에 따라 선 긋기 혹은 선 지우기.
            }
            if (other.GetComponent<DragScript>().dragging) //클릭 중일때 동작 안하고 연결 끊기
            {
                if (connected.Contains(other.gameObject))
                {
                    connected.RemoveAt(connected.IndexOf(other.gameObject));
                }
                other.GetComponent<PlayerBlock>().player.GetComponent<Player>().move = false;
                EnableLine();
            }
            if(GetComponent<DragScript>().dragging)
            {
                other.GetComponent<PlayerBlock>().player.GetComponent<Player>().move = false;
                connected.Clear();
                line.positionCount = 0;
                line.enabled = false;
            }        
        }

        if (other.CompareTag("ObstacleBlock"))
        {
            if (!GetComponent<DragScript>().dragging && !other.GetComponent<DragScript>().dragging)
            {
                MoveObstacle(other);
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
                        other.GetComponent<ObstacleBlock>().obstacleList[i].GetComponent<Obstacle>().move = false;
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
                        other.GetComponent<ObstacleBlock>().obstacleList[i].GetComponent<Obstacle>().move = false;
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
        if (other.CompareTag("PlayerBlock"))
        {            
            other.GetComponent<PlayerBlock>().player.GetComponent<Player>().move = false;            
            if (connected.Contains(other.gameObject))
            {
                connected.RemoveAt(connected.IndexOf(other.gameObject));
            }
            EnableLine();
        }
    }

    private void MoveObstacle(Collider2D other)
    {
        for (int i = 0; i < other.GetComponent<ObstacleBlock>().obstacleList.Count; i++)
        {
            if (other.GetComponent<ObstacleBlock>().obstacleList[i].GetComponent<Obstacle>().isMovable)
            {
                other.GetComponent<ObstacleBlock>().obstacleList[i].GetComponent<Obstacle>().move = true;
            }
        }

        if (!connected.Contains(other.gameObject))
        {
            connected.Add(other.gameObject);
        }
        particle.Play();
        EnableLine();
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
