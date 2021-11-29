using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TempMenu : MonoBehaviour
{
    public Text clickToStart;
    private int counter;
    public Toggle languageToggle;
    public bool introFin;
    public bool outro;
    public GameObject fader;
    private float timer;
    public ParticleSystem particle;

    private void Start()
    {
        if(PlayerPrefs.GetString("Lang") == "KR")
        {
            languageToggle.isOn = true;
            Debug.Log(PlayerPrefs.GetString("Lang"));
        }
        else
        {
            languageToggle.isOn = false;
            Debug.Log(PlayerPrefs.GetString("Lang"));
        }
        
    }

    void Update()
    {
        counter += 1;
        if(counter < 60)
        {           
            clickToStart.CrossFadeColor(Color.green, 1f, false, false);            
        }
        else if(counter < 120)
        {            
            if(counter > 90)
            {
                clickToStart.color = new Color(clickToStart.color.r, clickToStart.color.g, clickToStart.color.b, 0);
            }
            clickToStart.CrossFadeColor(Color.yellow, 1f, false, false);
            
        }
        if (counter > 120)
        {
            counter = 0;
            clickToStart.color = new Color(clickToStart.color.r, clickToStart.color.g, clickToStart.color.b, 1);
        }
        
        if (outro)
        {            
            timer += Time.deltaTime * 0.5f;
            GetComponent<AudioSource>().volume = Mathf.Lerp(GetComponent<AudioSource>().volume, 0, timer);
        }
    }

    private void OnMouseUp()
    {
        if(introFin)
        {
            fader.SetActive(true);            
            StartCoroutine(NextLevel());
        }
    }

    IEnumerator NextLevel()
    {
        outro = true;
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(1);
    }

    public void SetLanguage(bool On)
    {
        if(!On)
        {
            PlayerPrefs.SetString("Lang", "EN");
        }
        else
        {
            PlayerPrefs.SetString("Lang", "KR");
        }
    }  
}
