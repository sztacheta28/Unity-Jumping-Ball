using UnityEngine;
using System.Collections;

/// Simple Class that can be used to Destroy or Clean-Up gameObjects.
public class DestroyGameObject : MonoBehaviour
{
    public AudioClip destroySound, destroySoundTwo;	   
    public float delay;                       
    public bool destroyChildren;    
    public float pushChildAmount;                 

    void Start()
    {
        Transform[] children = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }

        if (!destroyChildren)
        {
            transform.DetachChildren();
        }

        for (int i = 0; i < children.Length; i++)
        {
            Transform child = children[i];
            if (child.GetComponent<Rigidbody>() && pushChildAmount != 0)
            {
                Rigidbody childRB = GetComponent<Rigidbody>();
                Vector3 pushDir = child.position - transform.position;
                childRB.AddForce(pushDir * pushChildAmount, ForceMode.Force);
                childRB.AddTorque(Random.insideUnitSphere, ForceMode.Force);
            }
        }

        if (destroySound)
        {
            AudioSource.PlayClipAtPoint(destroySound, transform.position);
        }

        if (destroySoundTwo)
        {
            AudioSource.PlayClipAtPoint(destroySoundTwo, transform.position);
        }

        Destroy(gameObject, delay);
    }
}