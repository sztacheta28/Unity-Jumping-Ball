using UnityEngine;
using System.Collections;

/// Simple Method that Disables a GameObject after delay time.
public class DisableGameObject : MonoBehaviour 
{
    public float delay;             

	void Start () 
	{
        Invoke("DisableObj",delay);
	}

    void DisableObj()
    {
        this.gameObject.SetActive(false);
    }
}