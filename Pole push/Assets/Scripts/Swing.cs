using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    Rigidbody rb;
    public float speed;

    public float angle;
    public float angleMin = -60;
    public float angleMax = 60;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rb.AddForce(new Vector3(1, 0, 0), ForceMode.VelocityChange);

        if (angle < angleMin)
        {
            angle = angleMin;
        }
        if (angle > angleMax)
        {
            angle = angleMax;
        }
        //transform.localEulerAngles = new Vector3(90, 0, angle);
    }
}
