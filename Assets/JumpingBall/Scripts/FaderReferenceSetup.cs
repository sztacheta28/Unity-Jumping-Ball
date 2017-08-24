using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// This Class setups FaderCanvas and the RawImage.
public class FaderReferenceSetup : MonoBehaviour 
{
    public RawImage faderRawImage;
    public GameObject parentCanvas;
    public static FaderReferenceSetup ourFaderReference;
    public static bool applicationIsQuiting;

    void Awake()
    {
        parentCanvas = this.transform.parent.gameObject;
        DontDestroyOnLoad(parentCanvas);
		
        if (ourFaderReference == null)
        {
            ourFaderReference = this;
        }
        else if (ourFaderReference != this)
        {
            Destroy(gameObject);
        }

        faderRawImage = this.GetComponent<RawImage>();
    }

    void OnDestroy()
    {
        applicationIsQuiting = true;
    }

}
