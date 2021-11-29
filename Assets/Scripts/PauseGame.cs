using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    public GameObject goButton;

    public void ResumeGame()
    {
        GameManager.Instance.softPause = false;
        goButton.SetActive(false);
    }
}
