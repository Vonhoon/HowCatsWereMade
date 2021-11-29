using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotSnap : MonoBehaviour
{
    public bool occupied;
    public GameObject occupiedBlock;

    private void OnTriggerStay2D(Collider2D other)
    {
        if(!occupied)
        {
            if (other.tag == "PlayerBlock" || other.tag == "MoveBlock" || other.tag == "EffectBlock" || other.tag == "ObstacleBlock" || other.tag == "ItemBlock")
            {                   
                if (other.GetComponent<DragScript>().dragging == false)
                {                    
                    occupiedBlock = other.gameObject;
                    occupied = true;
                }                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {     
        if (other.gameObject == occupiedBlock && occupied)
        {            
            occupied = false;
            occupiedBlock = null;
        }        
    }  
}
