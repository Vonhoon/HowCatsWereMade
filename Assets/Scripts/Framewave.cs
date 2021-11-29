using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Framewave : MonoBehaviour
{
    private Animator myAnim;
    private int counter;
    public int minSec;
    public int maxSec;
    [SerializeField] private int number; 

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        number = Random.Range(minSec * 60, (maxSec + 1) * 60);        
    }

    // Update is called once per frame
    void Update()
    {
        counter += 1;
        if(counter % number == 0)
        {            
            if(number % 2 == 0)
            {
                myAnim.SetTrigger("First");               
            }
            else
            {
                myAnim.SetTrigger("Second");                
            }
            number = Random.Range(minSec * 60, (maxSec + 1) * 60);
        }
    }
}
