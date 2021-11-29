using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;
    public float power;
    private Vector2 originalPos;
    private Stagetwo manager;

    void Start()
    {
        manager = FindObjectOfType<Stagetwo>();
        rb = GetComponent<Rigidbody2D>();
        direction = new Vector2(Random.Range(-1f, 1f), -1);
        originalPos = transform.position;
        rb.AddForce(direction * power, ForceMode2D.Impulse);        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            transform.position = originalPos;
            direction = new Vector2(Random.Range(-1f, 1f), -1);
            rb.velocity = Vector2.zero;
            rb.AddForce(direction * power, ForceMode2D.Impulse);            
            manager.StartVirusDialogue();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.position = originalPos;
            rb.velocity = Vector2.zero;
            direction = new Vector2(Random.Range(-1f, 1f), -1);
            rb.AddForce(direction * power, ForceMode2D.Impulse);
            manager.StartVirusDialogue();
        }
    }
}
