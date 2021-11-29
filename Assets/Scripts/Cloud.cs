using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : Obstacle
{
    public GameObject water;
    private int frames;
    [SerializeField] private int droptime;
    private Vector2 dropPos;
    [SerializeField] private int maxWater;
    [SerializeField] private List<GameObject> waterPool;
    private int currentNo;

    private void Start()
    {
        startGrav = rb.gravityScale;
        dropPos = transform.position;
        startPos = transform.position;
        GameManager.Instance.obstacles.Add(gameObject);

        for (int i = 0; i < maxWater; i++)
        {
            waterPool.Add(Instantiate(water, new Vector2(dropPos.x + i, dropPos.y + 10), Quaternion.identity));
            waterPool[i].SetActive(false);
        }
    }

    override public void FixedUpdate()
    {
        if(activate && !GameManager.Instance.softPause)
        {
            myAnim.SetBool("Activate", true);
            frames += 1;
            if(frames % droptime == 0 && currentNo < maxWater)
            {
                if(currentNo < maxWater)
                {
                    dropPos = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y);
                    waterPool[currentNo].transform.position = dropPos;
                    waterPool[currentNo].SetActive(true);
                    currentNo += 1;
                }                
            }
            else if(currentNo == maxWater)
            {
                currentNo = 0;
            }
        }
        else
        {
            myAnim.SetBool("Activate", false);
            for(int i = 0; i < maxWater; i++)
            {
                waterPool[i].SetActive(false);
                currentNo = 0;
            }
        }

        if (move && isMovable && !GameManager.Instance.softPause)
        {
            if (myType == type.bulldozer && activate || myType == type.drill || myType == type.cloud)
            {
                if (myDir == moveDir.right)
                {
                    transform.Translate(Vector2.right * Time.deltaTime * speed);
                }
                if (myDir == moveDir.left)
                {
                    transform.Translate(Vector2.left * Time.deltaTime * speed);
                }
                if (myDir == moveDir.up)
                {
                    transform.Translate(Vector2.up * Time.deltaTime * speed);
                }
                if (myDir == moveDir.down)
                {
                    transform.Translate(Vector2.down * Time.deltaTime * speed);
                }
            }
        }
        if (GameManager.Instance.softPause)
        {
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
        }
        else if (!GameManager.Instance.softPause && !noGrav)
        {
            rb.gravityScale = 1;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Wall"))
        {
            move = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Trap"))
        {
            gameObject.SetActive(false);
        }
    }
}
