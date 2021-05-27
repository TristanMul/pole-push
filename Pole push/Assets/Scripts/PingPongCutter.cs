using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongCutter : MonoBehaviour
{
    public Transform target;
    public Transform blades;

    IEnumerator Start()
    {
        var pointA = transform.position;
        var pointB = target.transform.position;
        while (true)
        {
            yield return StartCoroutine(MoveObject(transform, pointA, pointB, 1.6f));
            yield return StartCoroutine(MoveObject(transform, pointB, pointA, 1.6f));
        }
    }

    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f / time;
        while (i < 1.0f)
        {
            if (Movement.startGame)
            {
                i += Time.deltaTime * rate;
                thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            }
            yield return null;
        }
    }

    private void Update()
    {
        blades.transform.Rotate(Vector3.forward * Time.deltaTime * 300);
    }
}
