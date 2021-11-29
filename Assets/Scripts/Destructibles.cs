using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructibles : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.destructibles.Add(gameObject);   
    }

}
