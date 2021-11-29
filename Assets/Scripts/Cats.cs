using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cats : MonoBehaviour
{
    public Vector2 startPos;
    private Animator myAnim;
    public List<RuntimeAnimatorController> controllers;
    private Rigidbody2D rb;
    public GameObject catnip;
    [SerializeField] private float actionTime;
    [SerializeField] private float actionCounter;
    private enum State { idle, jump, run, catnip};
    [SerializeField] private State myState;
    private int rng;
    private int direction;
    private float speed;
    public GameObject emote;

    void Start()
    {
        startPos = transform.position;
        myAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        actionTime = 60;
        emote.SetActive(false);
    }

    private void Update()
    {
        actionCounter += 1;
        if(catnip.activeSelf)
        {
            myState = State.catnip;
        }
        if(actionCounter % actionTime == 0 && myState != State.catnip)
        {
            CancelAllActions();
            actionTime = Random.Range(60, 181);
            actionCounter = 0;
            rng = Random.Range(0, 3);
            if(rng == 0)
            {
                myState = State.idle;
            }
            else if(rng == 1)
            {
                myState = State.run;
                direction = Random.Range(0, 2);
                speed = Random.Range(0.2f, 1f);
            }
            else if(rng == 2)
            {
                myState = State.jump;
                direction = Random.Range(0, 2);
                speed = Random.Range(0.2f, 1f);
            }
        }        
    }

    void FixedUpdate()
    {
        if(myState == State.idle)
        {           
            myAnim.SetBool("Idle", true);
        }
        else if(myState == State.run)
        {            
            myAnim.SetBool("Run", true);            
            if (direction == 0)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                GetComponent<SpriteRenderer>().flipX = false;
            }
            if (direction == 1)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        else if (myState == State.jump)
        {            
            myAnim.SetBool("Run", true);
            if (direction == 0)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                GetComponent<SpriteRenderer>().flipX = false;
                if(actionCounter % 40 == 0)
                {
                    rb.AddForce(Vector2.up * Random.Range(3f, 6f), ForceMode2D.Impulse);
                }
            }
            if (direction == 1)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        if(myState == State.catnip)
        {
            if (actionCounter > 100)
            {
                myAnim.SetBool("Run", true);
                myAnim.SetBool("Catnip", false);
                if (transform.position.x - catnip.transform.position.x > 0)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                transform.position = Vector2.MoveTowards(transform.position, catnip.transform.position, Time.deltaTime * 2);
            }
            else
            {
                myAnim.SetBool("Catnip", true);
                myAnim.SetBool("Idle", false);
            }
        }
    }

    public void GiveOrders()
    {
        if(Random.Range(0f, 1f) > 0.8f)
        {
            actionCounter = actionTime;
        }
    }

    public void CancelAllActions()
    {
        myAnim.SetBool("Idle", false);
        myAnim.SetBool("Run", false);        
        myAnim.SetBool("Catnip", false);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Cat"))
        {
            transform.position = startPos;
            //randomly change skin and animation;
        }
        if(other.gameObject.CompareTag("Catnip"))
        {
            other.gameObject.SetActive(false);
            myState = State.idle;
            StartCoroutine(Heart());
        }
    }

    IEnumerator Heart()
    {
        emote.SetActive(true);
        yield return new WaitForSeconds(2f);
        emote.SetActive(false);
    }
}
