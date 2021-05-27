using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCol : MonoBehaviour
{
    public GameObject go;
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Pole"))
        {
            go.GetComponent<Animator>().enabled = false;
            go.GetComponent<EnemyWalk>().ragdoll = true;
            Destroy(GetComponent<Collider>());
            //go.GetComponent<EnemyWalk>().Ragdoll();
            Destroy(gameObject);
        }
        //if (col.CompareTag("Ragdoll"))
        //{
        //    if (col.GetComponent<Rigidbody>().velocity.magnitude > 1f)
        //    {
        //        go.GetComponent<Animator>().enabled = false;
        //        go.GetComponent<EnemyWalk>().ragdoll = true;
        //        Destroy(GetComponent<Collider>());
        //        go.GetComponent<EnemyWalk>().Ragdoll();
        //    }
        //}
    }
}
