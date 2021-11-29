using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour
{
    Text myText;
    public float speed;
    private bool fading;

    private void Start()
    {
        myText = GetComponent<Text>();
        Destroy(gameObject, speed + 1);
    }
    // Update is called once per frame
    void Update()
    {
        myText.CrossFadeAlpha(0, speed, true);
    }
}
