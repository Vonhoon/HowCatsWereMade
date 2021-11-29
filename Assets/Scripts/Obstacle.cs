using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public bool activate;
    public float power;
    public Animator myAnim;
    public Rigidbody2D rb;
    public Vector2 startPos;
    public Vector3 startEuler;
    public enum type { jumper, right, left, up, down, vault, drill, spikeBall, bulldozer, cloud, furnace, fan, platform, sliding };
    public type myType;
    public enum moveDir { right, left, up, down}
    public moveDir myDir;
    public bool hasSight;
    public bool isMovable;
    public bool move;
    public float speed;
    private GameObject trap;
    public bool carnivore;
    public string originalTag;
    public bool noGrav;
    public float startGrav;
    

    private void Start()
    {
        if(rb != null)
        {
            startGrav = rb.gravityScale;
        }        
        originalTag = gameObject.tag;
        if(myType == type.jumper || myType == type.vault || myType == type.drill || myType == type.spikeBall || myType == type.bulldozer || myType == type.platform)
        {            
            startPos = transform.position;
            startEuler = transform.localEulerAngles;
            GameManager.Instance.obstacles.Add(gameObject);
        }
    }

    public virtual void Update()
    {
        if(startPos == new Vector2(transform.position.x, transform.position.y))
        {
            if(trap != null && !trap.activeSelf)
            {
                trap.SetActive(true);
            }           
        }
        if (myType == type.drill)
        {
            if (activate)
            {
                myAnim.SetBool("Activated", true);
            }
            else
            {
                myAnim.SetBool("Activated", false);
            }
        }
        if(myType == type.fan)
        {
            if(activate)
            {
                myAnim.SetBool("TurnOn", true);
            }
            else
            {
                myAnim.SetBool("TurnOn", false);
            }
        }
        if(myType == type.sliding)
        {
            if(activate)
            {
                myAnim.SetBool("Activate", true);
            }
            else
            {
                myAnim.SetBool("Activate", false);
            }
        }
            
    }

    public virtual void FixedUpdate()
    {
        if(move && isMovable && !GameManager.Instance.softPause)
        {
            if (myType == type.bulldozer && activate || myType == type.drill || myType == type.cloud || myType == type.platform)
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
        if(GameManager.Instance.softPause && rb != null)
        {
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
        }
        else if (!GameManager.Instance.softPause && !noGrav && rb != null)
        {
            rb.gravityScale = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if((other.tag == "Player" || other.tag == "Enemy" || other.tag == "Predator") && activate)
        {
            if (myType == type.jumper)
            {
                if(GameManager.Instance.currentStage == 6)
                {
                    power = 8 * other.GetComponent<Player>().transform.localScale.x;                    
                }
                other.GetComponent<Player>().rb.AddForce(new Vector2(0.5f,1) * power, ForceMode2D.Impulse);
                StartCoroutine(Activate());
            }
            if(myType == type.fan)
            {
                other.GetComponent<Player>().myDirection = Player.direction.up;
                other.GetComponent<Player>().noGrav = true;                
            }
            if (myType == type.up)
            {
                other.GetComponent<Player>().myDirection = Player.direction.up;
                other.GetComponent<Player>().noGrav = true;
            }
            if (myType == type.down)
            {
                other.GetComponent<Player>().myDirection = Player.direction.down;
                other.GetComponent<Player>().noGrav = true;
            }
            if (myType == type.right)
            {
                other.GetComponent<Player>().myDirection = Player.direction.right;
                other.GetComponent<Player>().rb.gravityScale = 0;
            }
            if (myType == type.left)
            {
                other.GetComponent<Player>().myDirection = Player.direction.left;
                other.GetComponent<Player>().rb.gravityScale = 0;
            }
        }

        if(myType == type.drill)
        {
            if (activate && other.gameObject.CompareTag("Trap"))
            {
                trap = other.gameObject;
                other.gameObject.SetActive(false);
            }
        }

        if(myType == type.furnace)
        {
            if(activate && other.gameObject.CompareTag("Item"))
            {
                other.gameObject.GetComponent<Item>().myAnim.SetBool("Cooked", true);
                other.gameObject.GetComponent<Item>().ItemActivate = true;
                other.gameObject.GetComponent<CircleCollider2D>().enabled = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Destructable") && myType != type.drill)
        {        
            other.gameObject.GetComponent<Animator>().SetBool("Destroyed", true);
            if(myType == type.spikeBall)
            {
                gameObject.SetActive(false);
            }            
        }        
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (myType == type.drill)
        {
            if (activate && other.gameObject.CompareTag("Destructable"))
            {                
                other.gameObject.GetComponent<Animator>().SetBool("Destroyed", true);                
            }           
        }       
    }

    IEnumerator Activate()
    {
        myAnim.SetBool("Active", true);
        yield return new WaitForSeconds(0.5f);
        myAnim.SetBool("Active", false);
    }
}
