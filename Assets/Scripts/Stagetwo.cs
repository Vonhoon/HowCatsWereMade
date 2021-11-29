using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stagetwo : MonoBehaviour
{
    public GameObject virusSpawner;
    public Transform virusSpawnerMarker;
    private bool spawnCheck;
    private GameManager gm;
    public DialogueContainer virusTutorial;
    public Dialogue dialogueScript;    
    public bool virusTutoDone;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gm.softPause && !gm.dialogueStarted && !spawnCheck)
        {
            spawnCheck = true;
            Instantiate(virusSpawner, virusSpawnerMarker.position, Quaternion.identity, virusSpawnerMarker);
        }
    }

    public void StartVirusDialogue()
    {        
        if(!virusTutoDone)
        {            
            virusTutoDone = true;
            dialogueScript.gameObject.SetActive(true);
            dialogueScript.dialoguewindow.SetActive(true);
            dialogueScript.dialogue = virusTutorial;
            dialogueScript.page = 0;
            gm.dialogueStarted = true;
            dialogueScript.StartText();
        }
    }
}
