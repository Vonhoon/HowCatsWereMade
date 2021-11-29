using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<GameObject> characters = new List<GameObject>();
    public List<GameObject> obstacles = new List<GameObject>();
    public List<GameObject> destructibles = new List<GameObject>();
    public List<GameObject> items = new List<GameObject>();
    public int currentStage;
    public bool dialogueStarted;
    public bool softPause;
    private AudioManager audio;
    public string currentLang;
    public List<GameObject> liquidMakers = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this) 
        {
            Destroy(this);
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
        Application.targetFrameRate = 60;
        if(PlayerPrefs.GetString("Lang") == "KR")
        {
            currentLang = "KR";
        }
        else
        {
            currentLang = "EN";
        }
    }

    private void Start()
    {
        audio = GetComponent<AudioManager>();        
        audio.PlayAudio(currentStage);
    }

    private void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Restart();
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(ReloadLevel("clear", true));
        }
    }

    public void Restart()
    {
        for (int i = 0; i < destructibles.Count; i++)
        {
            destructibles[i].GetComponent<Animator>().SetBool("Destroyed", false);
        }
        for(int i = 0; i < characters.Count; i++)
        {
            characters[i].gameObject.SetActive(true);
            characters[i].GetComponent<Player>().rb.velocity = Vector2.zero;            
            characters[i].transform.position = characters[i].GetComponent<Player>().startPos;    
            characters[i].transform.localScale = characters[i].GetComponent<Player>().startScale;
            characters[i].GetComponent<Player>().myDirection = characters[i].GetComponent<Player>().startDirection;
        }
        for (int i = 0; i < obstacles.Count; i++)
        {
            obstacles[i].SetActive(true);
            obstacles[i].transform.position = obstacles[i].GetComponent<Obstacle>().startPos;            
            if (obstacles[i].GetComponent<Obstacle>().myType == Obstacle.type.spikeBall)
            {
                obstacles[i].GetComponent<Obstacle>().rb.velocity = Vector2.zero;
                obstacles[i].GetComponent<Obstacle>().rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                obstacles[i].transform.localEulerAngles = new Vector3(0, 0, 0);
                obstacles[i].GetComponent<Obstacle>().rb.constraints = RigidbodyConstraints2D.None;
            }
            if(obstacles[i].GetComponent<Obstacle>().myType == Obstacle.type.drill)
            {
                obstacles[i].transform.localEulerAngles = obstacles[i].GetComponent<Obstacle>().startEuler;
            }
        }        
        for (int i = 0; i < items.Count; i++)
        {
            items[i].SetActive(true);
            items[i].transform.position = items[i].GetComponent<Item>().startPos;            
        }
        if(liquidMakers.Count != 0)
        {
            for (int i = 0; i < liquidMakers.Count; i++)
            {
                StartCoroutine(liquidMakers[i].GetComponent<LiquidMaker>().Reset());
            }
        }
    }

    public IEnumerator ReloadLevel(string clear = "not clear", bool fast = false)
    {
        if(clear == "clear")
        {            
            currentStage += 1;
            dialogueStarted = true;            
        }        
        characters.Clear();
        obstacles.Clear();
        destructibles.Clear();
        items.Clear();
        liquidMakers.Clear();
        if(!fast)
        {
            yield return new WaitForSecondsRealtime(5f);
        }        
        audio.StopBGM();
        SceneManager.LoadScene(currentStage);
        Time.timeScale = 1;
        audio.PlayAudio(currentStage);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
