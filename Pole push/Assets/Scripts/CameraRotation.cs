using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public bool rotate;
    public float lerpDuration;
    public Vector3 targetRotation;

    // Start is called before the first frame update
    void Update()
    {
        if (rotate)
        {
            rotate = false;
            StartCoroutine("RotateToSide");
        }
    }

    IEnumerator RotateToSide()
    {
        Vector3 startRotation = transform.localEulerAngles;
        float timer = 0;
        while (timer < lerpDuration)
        {
            timer += Time.deltaTime;
            transform.localEulerAngles = Vector3.Lerp(startRotation, targetRotation, timer / lerpDuration);
            yield return null;
        }
        transform.localEulerAngles = targetRotation;
        yield return null;
    }
}
