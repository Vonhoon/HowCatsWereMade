using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seed : MonoBehaviour
{
    [SerializeField] private bool hydrated;
    private Animator myAnim;
    public GameObject sun;
    [SerializeField] private float distance;
    public GameObject winSequence;
    public GameObject shutter;
    private SpriteRenderer sprite;
    public GameObject goButton;
    private int myNumber;
    public GameObject alert;
    public GameObject canvas;
    public ParticleSystem particle;
    private bool nextStage;
    private bool died;
    private AudioManager audio;
    public GameObject helpButton;

    private void Start()
    {
        myAnim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        particle.Clear();
        audio = GameManager.Instance.GetComponent<AudioManager>();
    }  

    private void OnTriggerStay2D(Collider2D other)
    {
        if (hydrated && other.gameObject.CompareTag("NPC"))
        {
            distance = Vector2.Distance(transform.position, sun.transform.position);
            if (distance > 3 && !nextStage)
            {
                myAnim.SetBool("Grow", true);
                nextStage = true;
                StartCoroutine(OpenShutter(true));
                
            }
            else if (distance <= 3 && !died) 
            {
                died = true;
                if (alert != null)
                {
                    if(GameManager.Instance.currentLang == "KR")
                    {
                        alert.GetComponent<Text>().text = "사망: " + myNumber + "호 생명체\n타버림";
                    }
                    else
                    {
                        alert.GetComponent<Text>().text = "No. " + myNumber + " Cause of death:\nBurnt";
                    }
                    MakeAlert();
                }
            }
        }
        if (other.gameObject.CompareTag("Water"))
        {
            hydrated = true;
        }
    }

    IEnumerator OpenShutter(bool clear)
    {
        if(clear)
        {
            if(helpButton != null)
            {
                helpButton.SetActive(false);
            }    
            particle.Play();
            audio.StopBGM();
            audio.StageClear();
            yield return new WaitForSecondsRealtime(1f);
            winSequence.GetComponent<WinSequence>().StartSequence();
            sprite.enabled = false;
            yield return new WaitForSecondsRealtime(5f);
            shutter.SetActive(true);
            shutter.GetComponent<Animator>().SetTrigger("Close");            
            StartCoroutine(GameManager.Instance.ReloadLevel("clear"));
        }
        else
        {
            GetComponent<Collider2D>().enabled = false;
            sprite.enabled = false;
            shutter.SetActive(true);
            shutter.GetComponent<Animator>().SetTrigger("Close");
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(1.5f);
            goButton.SetActive(true);
            GameManager.Instance.Restart();
            GetComponent<Collider2D>().enabled = true;
            sprite.enabled = true;
            shutter.GetComponent<Animator>().SetTrigger("Open");            
            myNumber = Random.Range(myNumber + 1, myNumber + 1000);
            Time.timeScale = 1;
            GameManager.Instance.softPause = true;
            died = false;
        }
    }

    private void MakeAlert()
    {
        Vector2 anchoredPos;
        anchoredPos = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
        Instantiate(alert, anchoredPos, Quaternion.identity, canvas.transform);
    }

}
