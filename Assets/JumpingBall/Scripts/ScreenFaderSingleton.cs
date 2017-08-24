using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

/// Class that temporarily Fade the Screen, or Load/Restart a Scene. 
public class ScreenFaderSingleton : Singleton<ScreenFaderSingleton> {

    protected ScreenFaderSingleton(){ }

    public RawImage screenFadeTexture;
    public float fadeInDuration = 0.85f;
    public float fadeOutDuration = 0.95f;
    private float fadeInAlpha = 0;
    private float fadeOutAlpha = 1;
    private bool isPaused;
    private bool isPlayerQuiting;
    public GameObject faderPrefab;

    void Awake()
    {
        if (FaderReferenceSetup.ourFaderReference != null)
        {
            screenFadeTexture = FaderReferenceSetup.ourFaderReference.faderRawImage;
        }
        else
        {
            faderPrefab = TemporaryGameVars.screenFaderPrefab;
            Instantiate(faderPrefab, transform.position, transform.rotation);
            screenFadeTexture = FaderReferenceSetup.ourFaderReference.faderRawImage;
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

#if UNITY_5_3_OR_NEWER
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                FadeAndGoBack();
            }
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                FadeAndQuitApplication();
            }
#else
            if(Application.loadedLevel == 1)
            {
                FadeAndGoBack();
            }
            if(Application.loadedLevel == 0)
            {
                FadeAndQuitApplication();
            }

#endif

        }

        if (Input.GetKeyDown(KeyCode.Menu) || Input.GetKeyDown(KeyCode.M))
        {
            isPaused = !isPaused;

            if (!isPaused)
            {
                PauseGame();
            }
        }

    }

    public void PauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void DelayedFadeOut()
    {
        Invoke("FadeOut", 1.1f);
    }

    public void FadeAndLoadLevel()
    {
        Invoke("FadeOut", 2f);
        Invoke("LoadLevel", 3f);
        Invoke("FadeIn", 4f);
    }

    public void FadeAndLoadLevelFaster()
    {
        Invoke("FadeOut", 0f);
        Invoke("LoadLevel", 1f);
        Invoke("FadeIn", 3f);
    }

    public void FadeAndReloadLevel()
    {
        Invoke("FadeOut", 0f);
        Invoke("ZeroOutPlayerScore", 1.15f);
        Invoke("ReloadCurrentLevel", 1.25f);
        Invoke("FadeIn", 2.25f);
    }

    public void FadeAndLoadPreviousLevel()
    {
        Invoke("FadeOut", 0f);
        Invoke("ZeroOutPlayerScore", 1.15f);
        Invoke("ReturnToPreviousScene", 1.25f);
        Invoke("FadeIn", 2.25f);
    }

    public void FadeAndQuitApplication()
    {
        Invoke("FadeOut", 0f);
        Invoke("ExitApplication", 1.25f);
    }

    public void FadeAndGoBack()
    {
        Invoke("FadeOut", 0f);
        Invoke("ReturnToPreviousScene", 1.25f);
        Invoke("FadeIn", 2f);
    }

    private void ReloadCurrentLevel()
    {
#if UNITY_5_3_OR_NEWER
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
#else
        Application.LoadLevel(Application.loadedLevel);
#endif
    }

    private void ExitApplication()
    {

        if (Application.isPlaying)
        {
            Application.Quit();
        }

        
    }

    private void LoadLevel()
    {
#if UNITY_5_3_OR_NEWER
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(1);
        }
#else
        if(Application.loadedLevel == 0)
        {
            Application.LoadLevel(1);
        }
#endif
    }

    private void ReturnToPreviousScene()
    {
#if UNITY_5_3_OR_NEWER
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(0);
        }
#else
        if(Application.loadedLevel == 1)
        {
            Application.LoadLevel(0);
        }
#endif
    }

    public void ZeroOutPlayerScore()
    {
        TemporaryGameVars.playerScore = 0;
    }
	
    public void DebugSpawn()
    {
        //Debug.Log("spawn");
    }

    private void FadeIn()
    {
        if (screenFadeTexture != null)
        {
            screenFadeTexture.CrossFadeAlpha(fadeInAlpha, fadeInDuration, true);
        }
        else
        {
            screenFadeTexture = FaderReferenceSetup.ourFaderReference.faderRawImage;
            if (!FaderReferenceSetup.applicationIsQuiting)
            {
                screenFadeTexture.CrossFadeAlpha(fadeInAlpha, fadeInDuration, true);
            }
        }
    }

    private void FadeOut()
    {
        if (screenFadeTexture != null)
        {
            screenFadeTexture.CrossFadeAlpha(fadeOutAlpha, fadeOutDuration, true);
        }
        else
        {
            screenFadeTexture = FaderReferenceSetup.ourFaderReference.faderRawImage;
			
            if (!FaderReferenceSetup.applicationIsQuiting)
            {
                screenFadeTexture.CrossFadeAlpha(fadeOutAlpha, fadeOutDuration, true);
            }
        }
    }

    void OnEnable()
    {
        FadeIn();
    }

    void OnDisable()
    {
        FadeOut();
    }
}
