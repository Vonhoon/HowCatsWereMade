using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseShutter : MonoBehaviour
{
    public void Close()
    {
        gameObject.SetActive(false);
    }
}
