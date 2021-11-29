using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool ItemActivate;
    public Animator myAnim;
    public Vector2 startPos;
    public enum itemType { antidote, potion, meat, bug };
    public itemType myItem;
    public List<GameObject> players = new List<GameObject>();
    public Vector3 scaleChange;
    
    void Start()
    {
        startPos = transform.position;       
        GameManager.Instance.items.Add(gameObject);        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if((other.tag == "Player" || other.tag == "Enemy"))
        {
            if (myItem == itemType.antidote)
            {
                for(int i = 0; i < players.Count; i++)
                {
                    players[i].GetComponent<Player>().myItemEffect = Player.itemEffect.antidote;      
                }
            }
            if (myItem == itemType.potion)
            {
                for(int i = 0; i < players.Count; i++)
                {
                    players[i].GetComponent<Player>().transform.localScale += scaleChange;
                }
            }
            if (myItem == itemType.meat)
            {
                if(GameManager.Instance.currentStage == 7)
                {
                    other.gameObject.GetComponent<Player>().transform.localScale += scaleChange;
                    gameObject.SetActive(false);
                }
                else if (ItemActivate)
                {
                    other.gameObject.GetComponent<Player>().transform.localScale += scaleChange;                    
                    gameObject.SetActive(false);
                }
            }
            if (myItem == itemType.bug && !other.gameObject.GetComponent<Player>().carnivore)
            {
                other.gameObject.GetComponent<Player>().transform.localScale += scaleChange;
                gameObject.SetActive(false);
            }
        }        
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Predator"))
        {
            if(myItem == itemType.meat)
            {
                if (GameManager.Instance.currentStage == 7)
                {
                    other.gameObject.GetComponent<Player>().transform.localScale += scaleChange;
                    gameObject.SetActive(false);
                }
                else if (ItemActivate)
                {
                    other.gameObject.GetComponent<Player>().transform.localScale += scaleChange;
                    gameObject.SetActive(false);
                }
            }
        }
    }
}