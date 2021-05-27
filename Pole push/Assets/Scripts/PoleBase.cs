using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleBase : MonoBehaviour
{
    CameraFollow camF;
    CameraZoom camZ;

    public GameObject objectPool;
    public GameObject polePart;
    Rigidbody rb;

    public int size;

    GameObject lastPart;

    public int startSize;
    //public bool makeLonger;

    GameObject[] partPool;
    public int partPoolSize;

    GameObject tempPart;
    PolePart tempPolepart;
    int tempSize;

    public int alternateNr;
    public Material poleMat1;
    public Material poleMat2;

    // Start is called before the first frame update
    void Start()
    {
        camF = GameObject.FindGameObjectWithTag("camera").GetComponent<CameraFollow>();
        if (camF == null)
        {
            camZ = GameObject.FindGameObjectWithTag("camera").GetComponent<CameraZoom>();
        }
        lastPart = gameObject;
        partPool = GeneratePartPool(partPoolSize);
        rb = GetComponent<Rigidbody>();
        for (int i = 0; i < startSize; i++)
        {
            LengthenPole(false);
        }

        ReadSize();
    }

    public void GrowPole(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            LengthenPole(true);
        }
        ReadSize();
    }

    void LengthenPole(bool grow)
    {
        ReadSize();
        //Get a pole part from the pool
        tempPart = GetFromPool(partPool);

        //Check if the pool was empty and returned null
        if(tempPart != null)
        {
            //Make the part active
            tempPart.SetActive(true);
            //Set the new part kinematic if it isn't already
            tempPart.GetComponent<Rigidbody>().isKinematic = true;
            //Get the polePart script
            tempPolepart = tempPart.GetComponent<PolePart>();
            
            //Set the parent of the pole part
            tempPart.transform.parent = lastPart.transform;
            //Transform part in front of previous part
            //If grow is on grow the part from the previous part position
            if (grow && (!(lastPart == gameObject)))
            {
                tempPart.transform.localPosition = Vector3.zero;
                tempPolepart.GrowPole();
                //tempPolepart.SetJoint(rb);
            }
            else
            {
                tempPart.transform.localPosition = Vector3.up * 2;
                //tempPolepart.SetJoint(lastPart.GetComponent<Rigidbody>());
            }


            tempPart.transform.localEulerAngles = Vector3.zero;
            tempPart.transform.localScale = Vector3.one;

            //Tell the new part what the part before it is
            tempPolepart.SetPreviousPart(lastPart);
            
            //Set this new part as the last part of the pole
            lastPart = tempPart;

            if ((size + 1) % (2*alternateNr) >= alternateNr)
            {
                tempPart.GetComponent<MeshRenderer>().material = poleMat2;
            }
            else
            {
                tempPart.GetComponent<MeshRenderer>().material = poleMat1;
            }

            if (transform.childCount > 0 && grow)
            {
                transform.GetChild(0).GetComponent<PolePart>().StartCoroutine("LerpChain");
            }

        }
        //If null was returned give a debug message
        else
        {
            Debug.Log("PolePart pool is empty");
        }
    }

    public void SetLastPart(GameObject tempLastPart)
    {
        lastPart = tempLastPart;
    }


    //Create a pool of pole parts
    GameObject[] GeneratePartPool(int poolSize)
    {
        GameObject[] tempPool = new GameObject[poolSize];
        for (int i = 0; i < tempPool.Length; i++)
        {
            tempPool[i] = Instantiate(polePart);
            tempPool[i].SetActive(false);
            tempPool[i].transform.parent = objectPool.transform;
            tempPool[i].GetComponent<PolePart>().SetObjectPool(objectPool);
        }
        return tempPool;
    }

    //Returns the first gameObject that isn't active from the pool
    //If all gameObjects are active return null
    GameObject GetFromPool(GameObject[] tempPool)
    {
        for (int i = 0; i < tempPool.Length; i++)
        {
            if (!tempPool[i].activeInHierarchy)
            {
                return tempPool[i];
            }
        }
        return null;
    }

    public void ReadSize()
    {
        PolePart[] tempParts;
        tempParts = GetComponentsInChildren<PolePart>();
        size = tempParts.Length;
        UpdateCamZoom();
        if (size <= 0)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = true;
        }
    }

    private void UpdateCamZoom()
    {
        if (camF != null)
        {
            camF.SetZoom(size);
        }
        else
        {
            camZ.SetZoom(size);
        }
    }

    public void PlayerDie()
    {
        if (transform.childCount > 0)
        {
            transform.GetChild(0).GetComponent<PolePart>().BreakPole(false);
        }
        gameObject.SetActive(false);
    }

    public GameObject BreakMultiple(int nr)
    {
        ReadSize();
        if (size > nr)
        {
            tempSize = size;
            tempPart = transform.GetChild(0).gameObject;
            for (int i = 0; i < tempSize - nr; i++)
            {
                tempPart = tempPart.transform.GetChild(0).gameObject;
            }
            tempPart.GetComponent<PolePart>().BreakPole(false);
        }
        else
        {
            if (transform.childCount > 0)
            {
                transform.GetChild(0).GetComponent<PolePart>().BreakPole(false);
            }
        }
        ReadSize();
        return tempPart;
    }

    public int GetSize()
    {
        ReadSize();
        return size;
    }
}
