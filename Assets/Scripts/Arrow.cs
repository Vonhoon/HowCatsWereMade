using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.left * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

}
