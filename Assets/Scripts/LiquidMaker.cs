using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidMaker : MonoBehaviour
{
    public GameObject water;
    private int frames;
    [SerializeField] private int droptime;
    private Vector2 dropPos;
    [SerializeField] private int maxWater;
    private int currentNo;
    private List<GameObject> liquids = new List<GameObject>();

    private void Start()
    {
        GameManager.Instance.liquidMakers.Add(gameObject);
    }

    void FixedUpdate()
    {
        frames += 1;
        if (frames % droptime == 0)
        {
            if (currentNo < maxWater)
            {
                dropPos = new Vector2(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y);
                liquids.Add(Instantiate(water, dropPos, Quaternion.identity, transform));                
                currentNo += 1;
            }
        }      
    }

    public IEnumerator Reset()
    {
        foreach (GameObject liquid in liquids)
        {
            liquid.SetActive(false);
        }
        foreach (GameObject liquid in liquids)
        {            
            dropPos = new Vector2(transform.position.x + Random.Range(-0.2f, 0.2f), transform.position.y);
            liquid.transform.position = dropPos;
            if (liquid.TryGetComponent(out Lavadrop lava))
            {
                lava.changed = false;
                lava.GetComponent<Rigidbody2D>().isKinematic = false;
                lava.GetComponent<CircleCollider2D>().enabled = true;
                lava.GetComponent<SpriteRenderer>().color = Color.red;
            }
            yield return null;
            liquid.SetActive(true);          
        }
    }
}