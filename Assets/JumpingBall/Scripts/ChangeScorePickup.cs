using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// Class that handles the stars the player picks up.
public class ChangeScorePickup : MonoBehaviour
{
    public AudioClip scoreChangeSound; 
    public float localSoundVolume;  
    public Text scoreText;      
    public GameObject effectToInstantiate, effectToInstantiate2;   

    void Start()
    {
        localSoundVolume = TemporaryGameVars.soundVolume * 3f;
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IncreaseScore();
        }
    }

    public void IncreaseScore()
    {
        TemporaryGameVars.playerScore++;
        
        scoreText.text = TemporaryGameVars.playerScore.ToString();

        if (scoreChangeSound)
        {
            AudioSource.PlayClipAtPoint(scoreChangeSound, transform.position, localSoundVolume * 2f);
        }

        Instantiate(effectToInstantiate, transform.position, transform.rotation);
        Instantiate(effectToInstantiate2, transform.position, transform.rotation);

        gameObject.SetActive(false);
    }
}