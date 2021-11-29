using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryButton : MonoBehaviour
{
    public void Restart()
    {
        GameManager.Instance.Restart();
    }
}
