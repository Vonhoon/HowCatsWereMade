using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public bool move;
 
    public Rigidbody2D rb;
    public Vector2 startPos;
    public Vector3 startScale;
    private GameManager gm;
    public bool powered;
    public bool fired;
    private SpriteRenderer sprite;
    public enum direction {right, left, up, down};    
    public direction myDirection;
    public direction startDirection;    
    public float speed;
    private float initSpeed;
    public ParticleSystem particle;
    public GameObject shutter;
    private bool dying;
    public GameObject canvas;
    public GameObject alert;
    private int myNumber;
    public enum itemEffect {none, antidote} 
    public itemEffect myItemEffect; 
    private itemEffect startItemEffect;
    public GameObject winSequence;
    public GameObject goButton;
    public bool hasSight;
    public bool carnivore;
    public string originalTag;
    public bool noGrav;
    private float startGrav;
    private AudioManager audio;
    public DialogueContainer deathTexts;
    private List<string> deathTextsToShow;
    public GameObject helpButton; // 클리어시 끄기 위함
    // 0 마그마 화상 1 관통상 2 중독 사망 3 잡아먹힘 4 전기충격 5 화상 6 호 생명d
    public bool camouflage;
    public bool flippedX;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startGrav = rb.gravityScale;
        gm = GameManager.Instance;        
        startPos = transform.position;
        startScale = transform.localScale;
        gm.characters.Add(gameObject);
        sprite = GetComponent<SpriteRenderer>();    
        startDirection = myDirection;
        initSpeed = speed;
        startItemEffect = myItemEffect; 
        myNumber = Random.Range(1, 10);
        originalTag = gameObject.tag;
        if(deathTexts != null)
        {
            if (gm.currentLang == "KR")
            {
                deathTextsToShow = deathTexts.dialogues;
            }
            else
            {
                deathTextsToShow = deathTexts.dialoguesEN;
            }
        }
        if (particle != null)
        {
            particle.Clear();
        }
        audio = GameManager.Instance.GetComponent<AudioManager>();
    }

    private void Update()
    {
        if (gm.softPause)
        {
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
        }
        else if (!gm.softPause && !noGrav)
        {
            rb.gravityScale = startGrav;
        }
        else if (!gm.softPause && noGrav)
        {
            rb.gravityScale = 0;
        }
    }

    public virtual void FixedUpdate()
    {
        if(!gm.softPause)
        {
            if (move)
            {
                if (myDirection == direction.right)
                {
                    transform.Translate(Vector2.right * Time.deltaTime * speed);
                    if(flippedX)
                    {
                        sprite.flipX = true;
                    }
                    else
                    {
                        sprite.flipX = false;
                    }
                }
                if (myDirection == direction.left)
                {
                    transform.Translate(Vector2.left * Time.deltaTime * speed);
                    if (flippedX)
                    {
                        sprite.flipX = false;
                    }
                    else
                    {
                        sprite.flipX = true;
                    }
                }
                if (myDirection == direction.up)
                {
                    transform.Translate(Vector2.up * Time.deltaTime * speed);
                }
                if (myDirection == direction.down)
                {
                    transform.Translate(Vector2.down * Time.deltaTime * speed);
                }
            }
            if (powered)
            {
                sprite.color = Color.red;
            }
            else
            {
                sprite.color = Color.white;
            }
        }       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {     
        if(other.gameObject.CompareTag("Trap")) //lose
        {
            if(alert != null)
            {
                if (gm.currentStage == 3)
                {
                    alert.GetComponent<Text>().text = deathTextsToShow[7] + myNumber + deathTextsToShow[6] + "\n" + deathTextsToShow[0];
                    //0 마그마 화상 1 관통상 2 중독 사망 3 잡아먹힘 4 전기충격 5 화상
                }
                else
                {
                    alert.GetComponent<Text>().text = deathTextsToShow[7] + myNumber + deathTextsToShow[6] + "\n" + deathTextsToShow[1];
                }
                MakeAlert();
            }
            StartCoroutine(ShutterOpen(false));
            myDirection = startDirection;
            myItemEffect = startItemEffect;            
        }

        
        if ((other.gameObject.CompareTag("Poison")) && (myItemEffect != itemEffect.antidote)) //lose
        {
            if(alert != null)
            {
                alert.GetComponent<Text>().text = deathTextsToShow[7] + myNumber + deathTextsToShow[6] + "\n" + deathTextsToShow[2];
                MakeAlert();
            }
            StartCoroutine(ShutterOpen(false));
            myDirection = startDirection;
            myItemEffect = startItemEffect; 
        }

        if (other.gameObject.CompareTag("Predator") || other.gameObject.CompareTag("Dino") && (gameObject.CompareTag("Player") || gameObject.CompareTag("Enemy")))
        {
            if (alert != null)
            {
                alert.GetComponent<Text>().text = deathTextsToShow[7] + myNumber + deathTextsToShow[6] + "\n" + deathTextsToShow[3];
                MakeAlert();
            }
            StartCoroutine(ShutterOpen(false));
            myDirection = startDirection;
            myItemEffect = startItemEffect;
        }
        if (other.gameObject.CompareTag("Predator") && gameObject.CompareTag("NPC"))
        {
            if (alert != null)
            {
                alert.GetComponent<Text>().text = deathTextsToShow[7] + myNumber + deathTextsToShow[6] + "\n" + deathTextsToShow[3];
                MakeAlert();
            }
            gameObject.SetActive(false);           
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Player") && !other.gameObject.GetComponent<Player>().powered && !powered) //win
        {
            Destroy(other.gameObject);            
            move = false;            
            myDirection = startDirection;            
            myItemEffect = startItemEffect; 
            speed = initSpeed;
            if(particle != null)
            {
                particle.Play();
            }
            Time.timeScale = 0;
            StartCoroutine(ShutterOpen(true));
        }
        else if(other.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Player") && (other.gameObject.GetComponent<Player>().powered || powered))
        {
            if (alert != null)
            {
                alert.GetComponent<Text>().text = deathTextsToShow[7] + myNumber + deathTextsToShow[6] + "\n" + deathTextsToShow[4];
                MakeAlert();
            }
            StartCoroutine(ShutterOpen(false));            
            myDirection = startDirection;
            myItemEffect = startItemEffect; 
            speed = initSpeed;                      
        }
        if(other.gameObject.CompareTag("Enemy") && gameObject.CompareTag("Player") && (other.gameObject.GetComponent<Player>().fired || fired))
        {
            if (alert != null)
            {
                alert.GetComponent<Text>().text = deathTextsToShow[7] + myNumber + deathTextsToShow[6] + "\n" + deathTextsToShow[5];
                MakeAlert();
            }
            StartCoroutine(ShutterOpen(false));            
            myDirection = startDirection;
            myItemEffect = startItemEffect; 
            speed = initSpeed;                      
        }

        if (other.gameObject.CompareTag("Lava"))
        {
            if (alert != null)
            {
                alert.GetComponent<Text>().text = deathTextsToShow[7] + myNumber + deathTextsToShow[6] + "\n" + deathTextsToShow[0];
                MakeAlert();
            }
            StartCoroutine(ShutterOpen(false));
            myDirection = startDirection;
            myItemEffect = startItemEffect;
        }

        if (other.gameObject.CompareTag("Trap"))
        {
            if (alert != null)
            {
                if (gm.currentStage == 3)
                {
                    alert.GetComponent<Text>().text = deathTextsToShow[7] + myNumber + deathTextsToShow[6] + "\n" + deathTextsToShow[0];
                }
                else
                {
                    alert.GetComponent<Text>().text = deathTextsToShow[7] + myNumber + deathTextsToShow[6] + "\n" + deathTextsToShow[1];
                }
                MakeAlert();
            }
            StartCoroutine(ShutterOpen(false));
            myDirection = startDirection;          
            myItemEffect = startItemEffect; 
        }

        if (other.gameObject.CompareTag("Predator"))
        {
            if (alert != null)
            {                
                alert.GetComponent<Text>().text = deathTextsToShow[7] + myNumber + deathTextsToShow[6] + "\n" + deathTextsToShow[3];
                MakeAlert();
            }
            if (gameObject.CompareTag("NPC"))
            {
                gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(ShutterOpen(false));
                myDirection = startDirection;
                myItemEffect = startItemEffect;
            }
        }

        if ((other.gameObject.CompareTag("Poison")) && (myItemEffect != itemEffect.antidote))
        {
            if(alert != null)
            {
                alert.GetComponent<Text>().text = deathTextsToShow[7] + myNumber + deathTextsToShow[6] + "\n" + deathTextsToShow[2];
                MakeAlert();
            }
            StartCoroutine(ShutterOpen(false));
            myDirection = startDirection;
            myItemEffect = startItemEffect;        
        }
        if(gm.currentStage == 6)
        {
            if (other.gameObject.CompareTag("Destructable") && gameObject.transform.localScale.magnitude >= new Vector3(1.1f, 1.1f, 1.1f).magnitude)
            {
                other.gameObject.GetComponent<Animator>().SetBool("Destroyed", true);
            }
        }
        if(gm.currentStage == 9)
        {
            if (other.gameObject.CompareTag("Destructable"))
            {
                other.gameObject.GetComponent<Animator>().SetBool("Destroyed", true);
            }
        }
        if (other.gameObject.CompareTag("Arrow") && !camouflage)
        {
            if (alert != null)
            {                
                alert.GetComponent<Text>().text = deathTextsToShow[7] + myNumber + deathTextsToShow[6] + "\n" + deathTextsToShow[1];
                MakeAlert();
            }
            StartCoroutine(ShutterOpen(false));
            myDirection = startDirection;
            myItemEffect = startItemEffect;
        }
    }

    IEnumerator ShutterOpen(bool clear)
    {
        if(!dying)
        {
            if (shutter != null)
            {
                dying = true;
                if (clear)
                {
                    if(helpButton != null)
                    {
                        helpButton.SetActive(false);
                    }
                    winSequence.GetComponent<WinSequence>().StartSequence();
                    sprite.enabled = false;
                    audio.StopBGM();
                    yield return new WaitForSecondsRealtime(1f);
                    audio.StageClear();
                    yield return new WaitForSecondsRealtime(6f);
                    shutter.SetActive(true);
                    shutter.GetComponent<Animator>().SetTrigger("Close");
                    StartCoroutine(gm.ReloadLevel("clear"));
                    dying = false;
                }
                else
                {
                    GetComponent<Collider2D>().enabled = false;
                    sprite.enabled = false;
                    shutter.SetActive(true);
                    shutter.GetComponent<Animator>().SetTrigger("Close");
                    Time.timeScale = 0;
                    yield return new WaitForSecondsRealtime(1.5f);
                    if(goButton != null)
                    {
                        goButton.SetActive(true);
                    }
                    gm.Restart();
                    GetComponent<Collider2D>().enabled = true;
                    sprite.enabled = true;
                    shutter.GetComponent<Animator>().SetTrigger("Open");
                    dying = false;
                    myNumber = Random.Range(myNumber + 1, myNumber + 1000);
                    Time.timeScale = 1;
                    gm.softPause = true;
                }
            }
            else if (shutter == null)
            {
                dying = true;
                if(clear)
                {
                    yield return new WaitForSecondsRealtime(3f);
                    StartCoroutine(gm.ReloadLevel("clear"));
                    dying = false;                    
                }
                else
                {
                    GetComponent<Collider2D>().enabled = false;
                    sprite.enabled = false;
                    yield return new WaitForSecondsRealtime(1f);
                    gm.Restart();
                    sprite.enabled = true;
                    GetComponent<Collider2D>().enabled = true;
                    dying = false;
                    myNumber = Random.Range(myNumber + 1, myNumber + 1000);
                    gm.softPause = true;
                }
            }
        }        
    }

    private void MakeAlert()
    {
        Vector2 anchoredPos;
        anchoredPos = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
        Instantiate(alert, anchoredPos, Quaternion.identity, canvas.transform);
    }
}
