using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusSpawner : MonoBehaviour
{
    public GameObject virus;    
    public int maxVirus;
    public float spawnDelay;    

    private void Start()
    {
        StartCoroutine(Spawn());   
    }

    IEnumerator Spawn()
    {
        for(int i = 0; i < maxVirus; i++)
        {
            Instantiate(virus, transform.position, Quaternion.identity, transform);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
