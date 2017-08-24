using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// This class controls the background music.
public class BackGroundMenuAndMusic : MonoBehaviour
{
    public static BackGroundMenuAndMusic BGMusic;       
    public AudioSource aSource;  
    public float localSoundVolume;           
    public bool muteAudio; 

    void Start () 
	{
        if(BGMusic == null)
        {
            DontDestroyOnLoad(gameObject);
            BGMusic = this;
        }
        else if (BGMusic != this)
        {
            Destroy(gameObject);
        }

        localSoundVolume = TemporaryGameVars.soundVolume * 0.5f;
        aSource = GetComponent<AudioSource>();
        aSource.volume = localSoundVolume;

        if (TemporaryGameVars.mutedVolume == 1)
        {
            muteAudio = true;
            aSource.mute = true;
        }
        else if (TemporaryGameVars.mutedVolume == 0)
        {
            muteAudio = false;
            aSource.mute = false;
        }
    }

    public void MuteAudio()
    {
        muteAudio = !muteAudio;

        if (muteAudio)
        {
            aSource.mute = true;
            PlayerPrefs.SetInt("mutedAudio", 1);
        }
        else
        {
            aSource.mute = false;
            PlayerPrefs.SetInt("mutedAudio", 0);
        }
    }
}
