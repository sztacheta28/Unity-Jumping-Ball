using UnityEngine;
using System.Collections;

///Class that holds some of the Game/Player Data. 
public static class TemporaryGameVars{
    public static int playerScore = 0;

    public static int highestPlayerScore = PlayerPrefs.GetInt("highestPlayerScore");        

    public static float soundVolume = 0.2f;                                                

    public static GameObject screenFaderPrefab =                                            
        Resources.Load("Prefabs/FaderCanvas(DontDestroyOnLoad)")as GameObject;           

    public static int highestScoreAchieved = PlayerPrefs.GetInt("highestScore");      
	
    public static int mutedVolume = PlayerPrefs.GetInt("mutedAudio");                    
}
