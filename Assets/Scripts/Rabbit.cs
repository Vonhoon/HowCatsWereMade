using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    public GameObject meat;    
    public Transform meatPlaceholder;    
    private int rng = 3;
    [SerializeField] private int counter;
    [SerializeField] private int moveTime;
    [SerializeField] private float speed;
    [SerializeField] private float upMax;
    [SerializeField] private float upMin;
    public List<GameObject> objects;
    [SerializeField] private float distance;
    [SerializeField] private float distance2;
    private bool runaway;

    private void Start()
    {
        meat.SetActive(false);
    }

    private void Update()
    {        
        for (int i = 0; i < objects.Count; i++)
        {
            if(i == 0)
            {
                distance = Vector2.Distance(transform.position, objects[i].transform.position);
            }
            else
            {
                distance2 = Vector2.Distance(transform.position, objects[i].transform.position);
            }
        }
        if(distance > 3)
        {
            runaway = false;
        }
    }

    private void FixedUpdate()
    {        
        counter += 1;
        if(!runaway)
        {
            if (counter == moveTime)
            {
                if (distance > distance2)
                {
                    rng = 0;
                }
                else
                {
                    rng = 1;
                }
            }
            else if (counter > moveTime * 2)
            {
                rng = 3;
                counter = 0;
            }

            if (rng == 0)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                GetComponent<SpriteRenderer>().flipX = false;
            }
            if (rng == 1)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                GetComponent<SpriteRenderer>().flipX = true;
            }

            if (counter % 20 == 0)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * Random.Range(upMin, upMax), ForceMode2D.Impulse);
            }
        }
        else
        {
            transform.Translate(Vector2.right * speed * 4 * Time.deltaTime);
            GetComponent<SpriteRenderer>().flipX = true;
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Predator"))
        {
            meat.SetActive(true);
            meat.transform.parent = null;
            gameObject.SetActive(false);
        }
        if(other.CompareTag("Player"))
        {
            runaway = true;            
        }
    }
}
