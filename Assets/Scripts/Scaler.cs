using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField] private float maxSize;
    [SerializeField] private float minSize;
    private float x;
    private float t = 0f;
    private bool down;
    void Update()
    {                
        if (!down)
        {
            t += Time.unscaledDeltaTime * 0.5f;
            
            if (t >= 1f)
            {
                down = true;
            }
        }
        else if(down)
        {
            t -= Time.unscaledDeltaTime * 0.5f;
            if (t <= 0f)
            {
                down = false;
            }
        }
        x = Mathf.Lerp(minSize, maxSize, t);
        transform.localScale = new Vector3(x, x, 1);
    }
}
