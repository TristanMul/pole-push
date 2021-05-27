using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    Transform target;
    public float speed;
    public float distance;
    public bool ragdoll;
    public bool animChange;
    Vector3 dist;
    float norm;
    Vector3 targetPosition;
    //public List<GameObject> bodyparts = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //gameObject.GetComponent<Animator>().CrossFadeInFixedTime("Idle", 0.1f, -1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        dist = transform.position - target.position;
        norm = dist.sqrMagnitude;
        if (norm < distance && !ragdoll)
        {
            if (!animChange) { gameObject.GetComponent<Animator>().Play("Walk", -1);  animChange = true; }
            targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.LookAt(targetPosition);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        } 
    }

    //public void Ragdoll()
    //{
    //    foreach (GameObject go in bodyparts)
    //    {
    //        go.tag = "Ragdoll";
    //        go.layer = 0;
    //    }
    //}
}
