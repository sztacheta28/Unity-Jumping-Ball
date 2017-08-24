using UnityEngine;
using System.Collections;


/// Simple Script to Rotate GameObjects.
public class RotateObject : MonoBehaviour {

    public string obstacleRotationName;
    public Vector3 rotation;
    public Space space;
    public Rigidbody childRB;

    void OnEnable()
    {
        childRB = GetComponentInChildren<Rigidbody>();
    }

    void Update()
    {
        if (childRB)
        {
            Vector3 newRot = rotation * Time.deltaTime;
            Quaternion rot = Quaternion.Euler(newRot);
            childRB.MoveRotation(childRB.rotation * rot);
        }
        else
        {
            this.transform.Rotate(rotation * Time.deltaTime, space);
        }
    }
}
