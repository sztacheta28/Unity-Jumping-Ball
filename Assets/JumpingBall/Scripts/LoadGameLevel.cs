using UnityEngine;
using System.Collections;

/// Simple Class used to Load a level, while use of the fade in and fade out effect
public class LoadGameLevel : MonoBehaviour {
	
    public void PlayGameButton()
    {
        ScreenFaderSingleton.Instance.FadeAndLoadLevelFaster();
    }
	
}
