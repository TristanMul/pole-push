using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishStart : MonoBehaviour
{
    DragControll dc;
    CameraRotation cr;
    CameraZoom cz;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
        player = GameObject.FindGameObjectWithTag("Player");
        dc = GameObject.FindGameObjectWithTag("Canvas").transform.GetComponentInChildren<DragControll>();
        cr = player.transform.GetComponentInChildren<CameraRotation>();
        cz = player.transform.GetComponentInChildren<CameraZoom>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PoleBase"))
        {
            cr.StartCoroutine("RotateToSide");
            dc.LockControls();
            //cz.zoomOffset -= 5;
        }
    }
}
