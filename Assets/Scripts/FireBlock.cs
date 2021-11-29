using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBlock : MonoBehaviour
{
    private LineRenderer line;
    [SerializeField] private List<GameObject> connected = new List<GameObject>();
    public ParticleSystem particle;
    private GameManager gm;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        particle.Clear();
        gm = GameManager.Instance;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.CompareTag("ItemBlock")) //태그로 블록타입 구분
        {
            if (!GetComponent<DragScript>().dragging && !other.GetComponent<DragScript>().dragging) //클릭 중이 아닐때만 실행
            {
                for (int i = 0; i < other.GetComponent<ItemBlock>().ItemList.Count; i++)
                    other.GetComponent<ItemBlock>().ItemList[i].GetComponent<Item>().ItemActivate = true; //블록 스크립트를 통해 목표 옵젝 활성화

                if (!connected.Contains(other.gameObject)) // 선 긋기용 연결된 블록 리스트에 담기.
                {
                    connected.Add(other.gameObject); 
                }
                particle.Play();
                EnableLine(); //블록 리스트에 따라 선 긋기 또는 선지우기
            }
            if (other.GetComponent<DragScript>().dragging)
            {
                if (connected.Contains(other.gameObject))
                {
                    connected.RemoveAt(connected.IndexOf(other.gameObject));
                }
                for (int i = 0; i < other.GetComponent<ItemBlock>().ItemList.Count; i++)
                    other.GetComponent<ItemBlock>().ItemList[i].GetComponent<Item>().ItemActivate = false; //블록 스크립트를 통해 목표 옵젝 활성화
                EnableLine();
            }
            if (GetComponent<DragScript>().dragging)
            {
                for (int i = 0; i < other.GetComponent<ItemBlock>().ItemList.Count; i++)
                    other.GetComponent<ItemBlock>().ItemList[i].GetComponent<Item>().ItemActivate = false; //블록 스크립트를 통해 목표 옵젝 활성화

                connected.Clear();
                line.positionCount = 0;
                line.enabled = false;
            }
        }
        if(other.CompareTag("PlayerBlock"))
        {
            if (!GetComponent<DragScript>().dragging && !other.GetComponent<DragScript>().dragging)
            {
                other.GetComponent<PlayerBlock>().player.GetComponent<Player>().fired = true;
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
                other.GetComponent<PlayerBlock>().player.GetComponent<Player>().fired = false;
                EnableLine();
            }
            if (GetComponent<DragScript>().dragging)
            {
                other.GetComponent<PlayerBlock>().player.GetComponent<Player>().fired = false;
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
            other.GetComponent<PlayerBlock>().player.GetComponent<Player>().fired = false;
        }
        else if(other.CompareTag("ItemBlock"))
        {
            for (int i = 0; i < other.GetComponent<ItemBlock>().ItemList.Count; i++)
                other.GetComponent<ItemBlock>().ItemList[i].GetComponent<Item>().ItemActivate = false; //블록 스크립트를 통해 목표 옵젝 활성화
        }
        if (connected.Contains(other.gameObject))
        {
            connected.RemoveAt(connected.IndexOf(other.gameObject));
        }
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
