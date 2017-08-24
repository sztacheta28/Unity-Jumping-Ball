using UnityEngine;
using System.Collections;

/// This Class Scales an object over time by evaluating a Animation Curve.
public class AnimationCurveScaler : MonoBehaviour
{
    [Range(0.25f, 5f)]
    public float scaleSpeed = 0.25f;
    public AnimationCurve aCurve;
    private Transform _transform;
    private float step;
    private float objScale;

    public void Start()
    {
        _transform = this.transform;
    }

    void Update()
    {
        step += scaleSpeed * Time.deltaTime;
        objScale = aCurve.Evaluate(step);
        _transform.localScale = new Vector3(objScale, objScale, objScale);
        if (step >= 1)
        {
            step = 0;
        }
    }
}