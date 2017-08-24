using UnityEngine;
using System.Collections;

/// Simple Script which "Instantiates" the ScreenFaderSingleton.
public class FaderCaller : MonoBehaviour 
{

	void Start ()
	{
        ScreenFaderSingleton.Instance.DebugSpawn();
    }

}
