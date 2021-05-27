using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public bool lowered;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Stop")
        {
            gameObject.layer = 26;
            gameObject.tag = "Untagged";
            lowered = true;
            transform.GetChild(0).gameObject.layer = 26;
            if (transform.childCount == 2)
                transform.GetChild(1).gameObject.layer = 26;
        }
    }
}
