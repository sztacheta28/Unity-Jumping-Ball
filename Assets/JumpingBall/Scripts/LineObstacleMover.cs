using UnityEngine;
using System.Collections;

/// This Class controls the scrolling movement of the "line" type obstacles.
public class LineObstacleMover : MonoBehaviour
{
    public Transform startPosObj, endPosObj;
    public float moveToTime;
    public float moveBackFromTime;
    private Vector3 pointA, pointB;

    void OnEnable()
    {
        StartCoroutine(MoveStarter());
    }

    IEnumerator MoveStarter()
    {
        pointA = startPosObj.position;
        pointB = endPosObj.position;
        while (true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, moveToTime));
            yield return StartCoroutine(MoveObject(transform, pointB, pointA, moveBackFromTime));
        }
    }

    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }
}
