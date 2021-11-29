using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHand : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Animator myAnim;
    [SerializeField] private float duration;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        myAnim = GetComponent<Animator>();
        sprite.enabled = false;
    }

    public void TutorialHandOn()
    {
        sprite.enabled = true;
        myAnim.SetTrigger("TurnOn");     
    }  
}
