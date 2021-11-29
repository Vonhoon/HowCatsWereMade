using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinSequence : MonoBehaviour
{
    public ParticleSystem particle;
    public GameObject nextCreature;
    private bool turnOn;
    private float x;
    private float t;
    public List<GameObject> characters;
    [SerializeField] private float colorChangeTime;
    public GameObject gameArea;
    public Text winText;
    public DialogueContainer winTextDialogue;

    private void Start()
    {
        particle.Clear();
        if(GameManager.Instance.currentLang == "KR")
        {
            winText.text = winTextDialogue.dialogues[GameManager.Instance.currentStage - 1];
        }
        else
        {
            winText.text = winTextDialogue.dialoguesEN[GameManager.Instance.currentStage - 1];
        }
    }

    private void Update()
    {
        if (turnOn)
        {
            Mathf.Clamp(t += Time.unscaledDeltaTime * colorChangeTime, 0, 1);
            x = Mathf.Lerp(0, 1, t);
            Camera.main.backgroundColor = new Color(x, x, x);
        }

        float distance = Vector2.Distance(characters[0].transform.position, transform.position);
        if (distance < 0.2f && !turnOn)
        {
            turnOn = true;
            particle.Play();
            nextCreature.SetActive(true);
            Camera.main.backgroundColor = new Color(0, 0, 0);            
        }
    }

    public void StartSequence()
    {
        for(int i = 0; i < characters.Count; i++)
        {
            characters[i].GetComponent<SpiralMovement>().move = true;
        }
        gameArea.SetActive(false);
    }
}
