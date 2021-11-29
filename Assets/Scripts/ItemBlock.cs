using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBlock : MonoBehaviour
{
    public List<GameObject> ItemList = new List<GameObject>(); // 추가
    private LineRenderer line;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
    }

}
