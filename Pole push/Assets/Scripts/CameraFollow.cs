using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;

    [Header("Multiply the zoom effect")]
    public float zoomMult;
    [Header("The base pole parts for the zoom")]
    public int zoomOffset;
    public int zoomMin;
    public int zoomMax;

    float zoom;
    float currentZoom;

    // Start is called before the first frame update
    void Start()
    {
        //Get the current offset of the camera from the scene
        offset = transform.position - target.position;
        currentZoom = zoom;
    }

    // Update is called once per frame
    void Update()
    {
        //If the current zoom isn't the target zoom
        if (currentZoom != zoom)
        {
            //Lerp the current zoom towards the target zoom for smooth zooming
            LerpZoom();
        }
        //Update the camera position by tracking the player position and add a multiple of (0, 1, 1) vector for zooming
        transform.position = (target.position + offset + ((Vector3.up + Vector3.back) * (currentZoom + zoomOffset) * zoomMult));
    }

    //This function lerps the zoom value from the current value to the target value over time
    void LerpZoom()
    {
        //Lerp the zoom
        currentZoom += (zoom - currentZoom) * Time.deltaTime;
        //Snap the zoom if the difference is too small
        if (Mathf.Abs(currentZoom - zoom) < 0.01f)
        {
            currentZoom = zoom;
        }
    }

    //This function sets the target zoom
    public void SetZoom(int i)
    {
        //Set the target zoom
        zoom = i;

        //Keep the zoom between the zoomMin and zoomMax values
        if (zoom < zoomMin)
        {
            zoom = zoomMin;
        }
        if (zoom > zoomMax)
        {
            zoom = zoomMax;
        }
    }
}
