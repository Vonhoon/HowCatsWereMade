using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public DialogueContainer dialogue;
    public Text textToShow;
    public GameObject dialoguewindow;
    public int page;
    private GameManager gm;
    public GameObject shutter;
    private bool readmeOver;
    private int readmeCounter;
    public List <GameObject> readme;
    public List<GameObject> readmeEN;
    private bool newBlock;
    public GameObject showNewBlock;
    public GameObject goButton;
    public List<GameObject> allBlocks; //스테이지 시작 시 raycast 막기 위해 참조
    private AudioManager audio;
    private int soundQueue;
    private bool typingDone;
    private int audioFrameDelay;
    public List<string> selectedDialogue;
    public GameObject helpButton;
    public GameObject titleText;
    public GameObject resetButton;


    private void Start()
    {        
        gm = GameManager.Instance;
        audio = gm.GetComponent<AudioManager>();
        audioFrameDelay = 5;
        if(gm.currentStage != 1)
        {
            titleText.SetActive(false);
        }
        if (gm.currentStage == 2 && readme.Count != 0)
        {           
            readmeCounter = 0;
            if(gm.currentLang == "KR")
            {
                readme[readmeCounter].SetActive(true);
            }
            else
            {
                readmeEN[readmeCounter].SetActive(true);
            }
        }
        else
        {           
            if(titleText != null)
            {
                StartCoroutine(ShowTitle());
            }
            else
            {
                StartText();
            }
        }
        
    }

    private IEnumerator ShowTitle()
    {
        Time.timeScale = 0;
        gm.dialogueStarted = true;
        shutter.SetActive(true);
        titleText.SetActive(true);        
        yield return new WaitForSecondsRealtime(4f);
        titleText.SetActive(false);
        StartText();
    }
    
    public void StartText()
    {
        readmeOver = true;        
        shutter.GetComponent<Animator>().SetTrigger("Open");
        if (GameManager.Instance.currentLang == "KR")
        {
            selectedDialogue = dialogue.dialogues;
        }
        else
        {
            selectedDialogue = dialogue.dialoguesEN;
        }
        if(helpButton != null)
        {
            helpButton.SetActive(false);
        }
        Time.timeScale = 0;
        gm.dialogueStarted = true;
        page = 0;        
        StartCoroutine(TypeSentence(selectedDialogue[page]));
        
        if (goButton != null)
        {
            goButton.SetActive(false);
        }
        if (resetButton != null)
        {
            resetButton.SetActive(false);
        }
        for (int i = 0; i < allBlocks.Count; i++)
        {
            allBlocks[i].layer = 2; //ignore raycast
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        typingDone = false;
        textToShow.text = string.Empty;
        foreach (char letter in sentence.ToCharArray())
        {
            if(!typingDone)
            {
                soundQueue += 1;
                if (soundQueue % audioFrameDelay == 0)
                {
                    audio.Talk();
                    soundQueue = 0;
                }
                textToShow.text += letter;
                yield return null;
            }
        }
        typingDone = true;
    }

    private void OnMouseUp()
    {
        if(!readmeOver && readme.Count != 0)
        {
            if(readmeCounter < readme.Count - 1)
            {
                if(gm.currentLang == "KR")
                {
                    readme[readmeCounter].SetActive(false);
                    readme[readmeCounter + 1].SetActive(true);
                }
                else
                {
                    readmeEN[readmeCounter].SetActive(false);
                    readmeEN[readmeCounter + 1].SetActive(true);
                }
                readmeCounter += 1;
            }
            else
            {
                if(gm.currentLang == "KR")
                {
                    readme[readmeCounter].SetActive(false);
                }
                else
                {
                    readmeEN[readmeCounter].SetActive(false);
                }
                
                if(titleText != null)
                {
                    StartCoroutine(ShowTitle());
                }
                else
                {
                    StartText();
                }
            }
        }
        else if(readmeOver && !newBlock)
        {          
            if(typingDone)
            {
                page += 1;
                if (page != selectedDialogue.Count)
                {                    
                    StartCoroutine(TypeSentence(selectedDialogue[page]));
                }
                else if (page >= selectedDialogue.Count)
                {
                    gm.dialogueStarted = false;
                    dialoguewindow.SetActive(false);
                    if (gm.currentStage == 1)
                    {
                        FindObjectOfType<TutorialHand>().TutorialHandOn();
                    }
                    if (showNewBlock != null)
                    {
                        showNewBlock.SetActive(true);
                        newBlock = true;
                    }
                    else if (showNewBlock == null)
                    {
                        Time.timeScale = 1;
                        if (gm.currentStage != 1)
                        {
                            gm.softPause = true;
                            goButton.SetActive(true);                           
                        }
                        MakeBlocksClickAgain();                      
                        gameObject.SetActive(false);
                    }
                }
            }                       
            
            else if (!typingDone)
            {                
                StopCoroutine(TypeSentence(selectedDialogue[page]));
                typingDone = true;
                textToShow.text = string.Empty;                
                textToShow.text = selectedDialogue[page];
            }            
        }
        else if(newBlock == true && showNewBlock != null)
        {
            showNewBlock.SetActive(false);
            showNewBlock = null;
            Time.timeScale = 1;
            if(gm.currentStage != 1)
            {
                gm.softPause = true;
                goButton.SetActive(true);
            }
            newBlock = false;
            MakeBlocksClickAgain();
            gameObject.SetActive(false);            
        }
    }

    private void MakeBlocksClickAgain()
    {
        for(int i = 0; i < allBlocks.Count; i++)
        {
            allBlocks[i].layer = 0;
        }
        if(resetButton != null)
        {
            resetButton.SetActive(true);
        }
        if (helpButton != null)
        {
            helpButton.SetActive(true);
        }
    }
}
