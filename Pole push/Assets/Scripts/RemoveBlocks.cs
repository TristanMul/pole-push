using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBlocks : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Block" || other.gameObject.tag == "Bad")
        {
            other.gameObject.SetActive(false);
        }
    }
}
