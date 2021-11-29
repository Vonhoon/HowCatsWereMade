using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : Player
{
    public ParticleSystem sunParticle;
    public BoxCollider2D boxCollider;

    private void Awake()
    {
        sunParticle.Clear();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Obstacle"))
        {
            sunParticle.Clear();
            boxCollider.enabled = false;            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log(other.gameObject.name);
            sunParticle.Play();
            boxCollider.enabled = true;
            Camera.main.backgroundColor = new Color(0.3f, 0.4f, 0.4f);
        }
    }
}
