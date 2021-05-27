using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter : MonoBehaviour
{
    bool isMoving = true;
    public bool activated;
    public float speed;
    public GameObject blades;

    // Update is called once per frame
    void Update()
    {
        if (isMoving) { blades.transform.Rotate(Vector3.forward * Time.deltaTime * 300); }
        if (activated) { transform.position += Vector3.right * speed * Time.deltaTime; }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Stop"))
        {
            //isMoving = false;
            activated = false;
        }
    }
}
