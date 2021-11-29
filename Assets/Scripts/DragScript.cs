using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScript : MonoBehaviour
{

    public bool dragging;
    private float distance;    
    [SerializeField] private Vector2 originPos;
    public bool snapped;
    private bool isTouch;    
    private GameObject lastContact;
    [SerializeField] private bool tempDisable;
    private AudioManager audio;

    void Start()
    {         
        originPos = transform.position;
        audio = GameManager.Instance.GetComponent<AudioManager>();
    }
        
    void Update()
    {
        /*if (Input.touchCount > 0)
        {
            isTouch = true;
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                    {
                        deltaX = touchPos.x - transform.position.x;
                        deltaY = touchPos.y - transform.position.y;
                        dragging = true;
                    }
                    break;

                case TouchPhase.Moved:
                    if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                    {
                        rb.MovePosition(new Vector2(touchPos.x - deltaX, touchPos.y - deltaY));
                    }
                    break;

                case TouchPhase.Ended:
                    dragging = false;
                    isTouch = false;
                    break;
            }
        } */

        if (dragging && !isTouch)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);          
            Vector3 rayPoint = ray.GetPoint(distance);
            transform.position = rayPoint;            
        }
        if (!dragging && !snapped)
        {
            transform.position = originPos;
        }
        else if(!dragging && snapped)
        {
            transform.position = originPos;            
        }

    }

    void OnMouseDown()
    {
        if(!tempDisable)
        {
            distance = Vector3.Distance(transform.position, Camera.main.transform.position);
            dragging = true;
            audio.Pick();
        }
    }

    void OnMouseUp()
    {
        dragging = false;
        if(lastContact != null)
        {
            originPos = lastContact.transform.position;
            audio.Drop();
        }
        if (!snapped)
        {
            transform.position = originPos;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Slot" && !other.GetComponent<SlotSnap>().occupied)
        {
            lastContact = other.gameObject;            
            snapped = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Slot")
        {            
            snapped = false;
        }
    }
}
