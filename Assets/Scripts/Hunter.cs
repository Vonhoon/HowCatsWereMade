using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    public GameObject arrowObj;
    public Vector2 direction;
    public float distance;
    public int maxArrow;
    public int currentArrow;
    public GameObject dialogueManager;
    private bool startGame;
    private bool reloading;
    private bool shooting;

    void Start()
    {
        direction = Vector2.left;        
    }

    private void Update()
    {
        if(!dialogueManager.activeSelf)
        {
            startGame = true;
        }
    }

    void FixedUpdate()
    {
        if(!GameManager.Instance.softPause && startGame)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance);

        if (hit)
        {
            if (hit.collider.gameObject.CompareTag("Player") && hit.collider.gameObject.GetComponent<Player>().camouflage == false)
            {
                if (currentArrow <= 0)
                {
                    reloading = true;
                    StartCoroutine(Reloading());                    
                }
                else
                {
                    StartCoroutine(Shooting());
                }
            }
        }  
        else
        {
            Debug.DrawRay(transform.position, direction * distance, Color.green);
        }
    }

    IEnumerator Reloading()
    {
        if(reloading && !shooting)
        {
            reloading = false;
            //reloading motion later
            yield return new WaitForSeconds(3f);          
            currentArrow = maxArrow;            
        }
    }

    IEnumerator Shooting()
    {
        if(!shooting)
        {
            shooting = true;
            GetComponent<Animator>().SetBool("Shoot", true);
            yield return new WaitForSeconds(1.5f);
            Debug.DrawRay(transform.position, direction * distance, Color.red);
            Instantiate(arrowObj, transform.position, transform.rotation);
            currentArrow -= 1;
            yield return new WaitForSeconds(0.5f);
            GetComponent<Animator>().SetBool("Shoot", false);
            shooting = false;
        }
    }
}
