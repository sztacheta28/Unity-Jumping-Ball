using UnityEngine;
using System.Collections;

/// Class that handles the colorChange items the player picks up.
public class ChangeColorPickup : MonoBehaviour
{
    public AudioClip colorChangeSound;                      
    public float localSoundVolume;                          
    public PlayerController pController;                    
    public ColorColliderController cColliderController;     

    void Start()
    {
        localSoundVolume = TemporaryGameVars.soundVolume * 2f;
        pController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        cColliderController = GameObject.FindGameObjectWithTag("ColliderController").GetComponent<ColorColliderController>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangePlayerColor();
        }

    }

    public void ChangePlayerColor()
    {
        pController.SwitchPlayerColor();
        cColliderController.ChangeColorColliderState();
        cColliderController.IncrementObstacleProgression();

        if (colorChangeSound)
        {
            AudioSource.PlayClipAtPoint(colorChangeSound, transform.position, localSoundVolume * 2f);
        }

        this.gameObject.SetActive(false);
    }
}
