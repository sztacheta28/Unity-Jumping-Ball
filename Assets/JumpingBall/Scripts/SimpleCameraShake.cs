using UnityEngine;
using System.Collections;

/// Method that "shakes" the camera transform
public class SimpleCameraShake : MonoBehaviour {

    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;
    public Transform shakeCam;
    public string shakeCameraTag;

    void Start () 
	{
        shakeCam = GetComponent<Camera>().transform;
    }

    public void StartShake()
    {
        StopAllCoroutines();
        StartCoroutine("Shake");
    }

    IEnumerator Shake()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;

            float percentComplete = elapsedTime / shakeDuration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            float x = Random.value * 2.0f - 1.0f; //-1..1
            float y = Random.value * 2.0f - 1.0f; //-1..1

            x *= shakeMagnitude * damper;
            y *= shakeMagnitude * damper;
            y *= shakeMagnitude * damper;

            shakeCam.position = new Vector3(shakeCam.position.x + x, shakeCam.position.y + y, shakeCam.position.z);

            yield return null;
        }
    }
}
