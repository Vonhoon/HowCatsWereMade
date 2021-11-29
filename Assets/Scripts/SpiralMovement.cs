using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralMovement : MonoBehaviour
{

    private Vector3 direction;
    public Transform center;
    private float distanceThisFrame;
    [SerializeField] private float speed;
    public bool move;
    private float distance;

    private void Update()
    {
        if (move)
        {
            distance = Vector2.Distance(transform.position, center.position);
            if(distance < 1f)
            {
                speed = 1f;
            }
            direction = center.position - transform.position;
            direction = Quaternion.Euler(0, 0, 70) * direction;
            distanceThisFrame = speed * Time.unscaledDeltaTime;
            transform.Translate(direction.normalized * distanceThisFrame, Space.World);
            if(distance < 0.1f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
