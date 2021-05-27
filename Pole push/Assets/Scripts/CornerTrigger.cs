using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerTrigger : MonoBehaviour
{
    [Header("True for left corner false for right")]
    public bool leftCorner;

    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerTrigger"))
        {
            other.GetComponentInParent<Movement>().TurnCorner(leftCorner);
        }
        if (other.CompareTag("ProgressBar"))
        {
            other.GetComponent<BarMovement>().TurnCorner(leftCorner);
        }
    }
}
