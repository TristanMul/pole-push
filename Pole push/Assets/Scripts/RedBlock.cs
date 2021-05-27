using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBlock : MonoBehaviour
{
    public int breakForce;
    public float expForce = 500f;
    public float expRad = 10f;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Pole"))
        {
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(0).transform.gameObject.SetActive(true);
            other.gameObject.GetComponentInParent<PoleBase>().BreakMultiple(breakForce);
            /*other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(
                expForce * other.gameObject.GetComponentInParent<Rigidbody>().mass * 2.5f,
                new Vector3(other.transform.position.x, other.transform.position.y - 1f, other.transform.position.z), expRad);*/
        }
    }
}
