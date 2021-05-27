using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBridge : MonoBehaviour
{
    Vector3 startRotation;
    [Header("The rotation of the bridge in its closed state")]
    public Vector3 targetRotation;
    float timePast;
    [Header("How long the closing takes")]
    public float closedDuration;
    public bool hasClosed;

    //Tell the bridge piece to close
    public void Close()
    {
        //Prevent multiple closings
        if (!hasClosed)
        {
            //Start the closing animation
            StartCoroutine("CloseAni");
            hasClosed = true;
        }
    }

    //Closing animation fuction
    IEnumerator CloseAni()
    {
        //Get the start angle
        startRotation = transform.localEulerAngles;
        //Reset time past just in case
        timePast = 0;

        //While the time past hasn't surpassed the fall duration keep lerping
        while (timePast < closedDuration)
        {
            //Lerp the angle between start and target angles based on time past
            transform.localEulerAngles = Vector3.Lerp(startRotation, targetRotation, timePast / closedDuration);
            //Keep track of time passing
            timePast += Time.deltaTime;
            //Wait a frame before going on
            yield return null;
        }
        //Set the angle after the duration
        transform.localEulerAngles = targetRotation;
        yield return null;
    }
}
