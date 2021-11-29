using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catnip : MonoBehaviour
{
    public GameObject catnip;
    private Vector2 startPos;
    private Vector2 dropPos;

    private void Start()
    {
        startPos = catnip.transform.position;
    }

    private void OnMouseDown()
    {
        if(!catnip.activeSelf)
        {
            dropPos = new Vector2(startPos.x + Random.Range(-1f, 1f), startPos.y);
            catnip.transform.position = dropPos;
            catnip.SetActive(true);
        }        
    }

    public void QuitGame()
    {
        GameManager.Instance.QuitApp();
    }
}
