using UnityEngine;
using UnityEngine.UI;
using System.Collections;



/// Color values
public enum PlayerColor
{
    Pink, Purple, Teal, Yellow
};

/// The Player Controller Class which Controls Player Color, Position, State, Instantiates the Death Effects and Enables the EndGameCanvas.
public class PlayerController : MonoBehaviour
{
    public bool testingMode;                            
    public Rigidbody playerRB;
    public ForceMode upwardForceType;          
    public float upwardForce;                           
    public bool playerActivated;            
    public bool isPlayerDead;             
    public PlayerColor playerColor;                     
    public Color[] playerColorArray;                    
    public Color originalColor;                         
    private Renderer playerRenderer;                   
    public int numOfColors;                            
    public AudioClip deathSound;                       
    private float localSoundVolume;                     
    public GameObject deathParticles, flashObject;      
    private Camera mainCam;                             
    private ChromaticAberration chromo;                
    private SimpleCameraShake shake;                   
    private float buttonCount = 1.5f;               
    private Animator pAnimator;                       
    private Canvas endGameCanvas;          
    public float shakeRate = 4.0F;
    private float nextShake = 0.0F;

    void Awake()
    {
        mainCam = Camera.main;
        endGameCanvas = GameObject.FindGameObjectWithTag("EndGameCanvas").GetComponent<Canvas>();
        endGameCanvas.enabled = false;
        pAnimator = gameObject.GetComponentInChildren<Animator>();
        localSoundVolume = TemporaryGameVars.soundVolume * 5f;
        playerRenderer = gameObject.GetComponentInChildren<Renderer>();
        playerRenderer.material.color = playerColorArray[(int)playerColor];
        playerRB = GetComponent<Rigidbody>();
        playerRB.useGravity = false;
    }

    void Start()
    {
        chromo = mainCam.GetComponent<ChromaticAberration>();
        shake = mainCam.GetComponent<SimpleCameraShake>();
        numOfColors = System.Enum.GetValues(typeof(PlayerColor)).Length;
    }

    void Update()
    {
        if (nextShake < Time.time)
        {
            shake.StartShake();
            nextShake = Time.time + shakeRate;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!playerActivated)
            {
                playerActivated = true;
            }
            if (playerActivated && !endGameCanvas.enabled)
            {
                playerRB.useGravity = true;
                pAnimator.SetTrigger("Jump");
                playerRB.AddForce(Vector3.up * upwardForce, upwardForceType);
            }
        }

        if (Input.GetKey(KeyCode.U) && Input.GetKey(KeyCode.I))
        {
            buttonCount -= Time.deltaTime;
            if (buttonCount < 0)
            {
                DebugMode();
                buttonCount = 1.5f;
            }
        }
        else
        {
            buttonCount = 1.5f;
        }

    }

    public void DebugMode()
    {
        testingMode = !testingMode;
    }

    public void SwitchPlayerColor()
    {
        playerColor += 1;
        int playerColorsInt = (int)playerColor;
        if(playerColorsInt == numOfColors)
        {
            playerColor = 0;
        }

        switch (playerColor)
        {
            case PlayerColor.Pink:
                playerRenderer.material.color = playerColorArray[0];

                break;
            case PlayerColor.Purple:
                playerRenderer.material.color = playerColorArray[1];

                break;
            case PlayerColor.Teal:
                playerRenderer.material.color = playerColorArray[2];

                break;
            case PlayerColor.Yellow:
                playerRenderer.material.color = playerColorArray[3];

                break;
            default:
                //Debug.Log("Color is Broken");
                break;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (!testingMode)
        {
            PlayerDeathEffectsOnly();
            Invoke("ShowEndGameCanvasAndScore", 1.75f);
        }
    }

    void ShowEndGameCanvasAndScore()
    {
        if(TemporaryGameVars.highestPlayerScore < TemporaryGameVars.playerScore)
        {
            UpdateHighestScore(TemporaryGameVars.playerScore);
        }

        playerRB.useGravity = false;
        playerRB.velocity = new Vector3(0, 0, 0);
        endGameCanvas.enabled = true;
    }

    public void UpdateHighestScore(int amt)
    {
        TemporaryGameVars.highestPlayerScore = amt;
        PlayerPrefs.SetInt("highestPlayerScore", TemporaryGameVars.playerScore);
    }

    public void PlayerDeathEffectsOnly()
    {
        chromo.StartAbberation();
        shake.StartShake();

        if (deathSound)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position, localSoundVolume * 2f);
        }
		
        Instantiate(deathParticles, transform.position, transform.rotation);
        Instantiate(flashObject, transform.position + new Vector3(0, 7f, 0), transform.rotation);
        gameObject.SetActive(false);
        isPlayerDead = true;
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}
