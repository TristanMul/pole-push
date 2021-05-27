using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverAnimation : MonoBehaviour
{
    bool hasFallen;
    bool forward;

    Vector3 startRotation;
    Vector3 targetRotation;
    public float fallAngle;

    float timePast;
    public float fallDuration;

    //Tell the lever to fall forwards
    public void FallForward()
    {
        if (!hasFallen)
        {
            //Prevent multiple falls
            hasFallen = true;

            //Fall forwards
            forward = true;
            //Start falling animation
            StartCoroutine("FallOver");
        }
    }

    //Tell the lever to fall backwards
    public void FallBackward()
    {
        if (!hasFallen)
        {
            //Prevent multiple falls
            hasFallen = true;

            //Fall backwards
            forward = false;
            //start falling animation
            StartCoroutine("FallOver");
        }
    }

    //Falling animation fuction
    private IEnumerator FallOver()
    {
        //Get the starting rotation of the switch
        startRotation = transform.localEulerAngles;
        //Set this as the baseline for the target
        targetRotation = startRotation;
        //Depending on the falling direction set the target x angle
        if (forward)
        {
            targetRotation.x = fallAngle;
        }
        else
        {
            targetRotation.x = -fallAngle;
        }
        //Reset time past jsut in case
        timePast = 0;

        //While the time past hasn't surpassed the fall duration keep lerping
        while (timePast < fallDuration)
        {
            //Lerp the angle between start and target angles based on time past 
            transform.localEulerAngles = Vector3.Lerp(startRotation, targetRotation, timePast / fallDuration);
            //Keep track of time passing
            timePast += Time.deltaTime;
            //Wait a frame before going on
            yield return null;
        }
        //Set the angle after the duration has past
        transform.localEulerAngles = targetRotation;
        yield return null;
    }
    


}
