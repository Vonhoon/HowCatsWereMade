using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public ParticleSystem particle;
    private Animator myAnim;
    public GameObject seed;

    private void Start()
    {
        particle.Clear();
        myAnim = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            myAnim.SetBool("Grow", true);
            particle.Play();
        }
        if (other.gameObject.CompareTag("Player"))
        {
            seed.SetActive(true);
            seed.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
        }
    }
}
