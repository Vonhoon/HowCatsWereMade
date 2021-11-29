using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{

    public GameObject helpWindow;
    private GameManager gm;
    public Button pauseButton;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
    }

    public void OpenHelpWindow()
    {
        helpWindow.SetActive(true);
    }

    public void CloseHelpWindow()
    {
        helpWindow.SetActive(false);
    }

    public void RestartStage()
    {
        gm.Restart();
    }

    public void RestartAll()
    {
        StartCoroutine(gm.ReloadLevel());
    }
}
