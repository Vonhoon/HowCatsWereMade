using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunlight : MonoBehaviour
{
    public GameObject oxygen;
    public GameObject background;
    public ParticleSystem particle;
    public GameObject trap;
    public GameObject smoke;
    public GameObject vault;
    private bool switched;
    private bool inRange;
    public DialogueContainer sunlightScript;
    public Dialogue dialogueScript;
    private bool firstmelt;
    public ParticleSystem activateParticle;

    private void Start()
    {
        particle.Clear();
        activateParticle.Clear();
    }

    private void FixedUpdate()
    {
        if(!GameManager.Instance.softPause)
        {
            if (!vault.GetComponent<Vault>().activate && switched)
            {
                switched = false;
                background.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.05f);
                oxygen.SetActive(false);
                particle.Clear();
                trap.SetActive(true);
                vault.GetComponent<Animator>().SetTrigger("Close");
            }

            if (vault.GetComponent<Vault>().activate)
            {
                particle.Play();
            }
            else
            {
                particle.Clear();
            }

            if (vault.GetComponent<Vault>().activate && inRange && !switched)
            {
                switched = true;
                background.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.2f);
                oxygen.SetActive(true);
                StartCoroutine(MeltTrap());

                if (!firstmelt)
                {
                    activateParticle.Play();
                    firstmelt = true;
                    dialogueScript.gameObject.SetActive(true);
                    dialogueScript.dialoguewindow.SetActive(true);
                    dialogueScript.dialogue = sunlightScript;
                    dialogueScript.page = 0;
                    GameManager.Instance.dialogueStarted = true;
                    dialogueScript.StartText();
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            inRange = true;
            other.gameObject.GetComponent<Player>().move = false;
        }
        if (vault.GetComponent<Vault>().activate && inRange)
        {            
            if (other.gameObject.CompareTag("Player") && !switched)
            {               
                other.gameObject.GetComponent<Player>().move = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            inRange = false;
            switched = false;            
        }
    }

    IEnumerator MeltTrap()
    {
        smoke.SetActive(true);
        yield return new WaitForSeconds(1f);
        smoke.SetActive(false);
        trap.SetActive(false);
    }
}
