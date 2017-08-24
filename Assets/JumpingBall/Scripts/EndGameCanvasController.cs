using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// This Class Updates the player scores on the EndGameCanvas, and Holds the MainMenu and Replay Button Methods.
public class EndGameCanvasController : MonoBehaviour 
{
    private Canvas endGameCan;               
    public Text currentScore, highestScore;   

    void Awake()
    {
        currentScore = GameObject.Find("CurrentScoreField").GetComponent<Text>();
        highestScore = GameObject.Find("HighestScoreField").GetComponent<Text>();
    }

    void Start () 
	{
        endGameCan = GetComponent<Canvas>();
    }

    void Update () 
	{
        if(endGameCan.enabled == true)
        {
            highestScore.text = TemporaryGameVars.highestPlayerScore.ToString();
            currentScore.text = TemporaryGameVars.playerScore.ToString();
        }
    }

    public void ReplayButtonPress()
    {
        ScreenFaderSingleton.Instance.FadeAndReloadLevel();
    }

    public void MainMenuButtonPress()
    {
        ScreenFaderSingleton.Instance.FadeAndLoadPreviousLevel();
    }
}