using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWall : MonoBehaviour
{
    public bool destroy;
    public float expForce;
    public float expRad;

    public float despawnTime;

    public int angVelMut;
    public int nrPieces;

    Vector3 expPos;
    Vector3 tempPos;
    Vector3 randomVect;

    GameObject wallIntact;
    GameObject[] wallPieces;
    Rigidbody[] pieceRB;

    Rigidbody tempRB;

    // Start is called before the first frame update
    void Start()
    {
        wallPieces = new GameObject[nrPieces];
        pieceRB = new Rigidbody[nrPieces];
        expPos = transform.position;
        tempPos = expPos;
        tempPos.y = 0;
        tempPos.x += 1;
        expPos.y -= transform.localScale.y * 0.25f;
        expPos.x += (Random.value - 0.5f) * 0.1f;
        expPos.z -= 0.5f;

        wallIntact = transform.GetChild(0).gameObject;
        for (int i = 1; i < transform.childCount; i++)
        {
            wallPieces[i - 1] = transform.GetChild(i).gameObject;
            pieceRB[i - 1] = wallPieces[i - 1].GetComponent<Rigidbody>();
            wallPieces[i - 1].SetActive(false);
        }
    }

    public void DestroyWall(GameObject tempPart)
    {
        wallIntact.SetActive(false);
        for (int i = 0; i < wallPieces.Length; i++)
        {
            wallPieces[i].SetActive(true);
            wallPieces[i].transform.parent = null;
            pieceRB[i].AddExplosionForce(expForce, expPos, expRad);
            randomVect.x = (Random.value - 0.5f) * angVelMut;
            randomVect.y = (Random.value - 0.5f) * angVelMut;
            randomVect.z = (Random.value - 0.5f) * angVelMut;
            pieceRB[i].angularVelocity = randomVect;
        }
        tempRB = tempPart.GetComponent<Rigidbody>();
        tempRB.AddExplosionForce(expForce * tempRB.mass * 2.5f, tempPos, expRad);
        StartCoroutine("DespawnAfterTime");
    }

    IEnumerator DespawnAfterTime()
    {
        /*
        Vector3 startScale = transform.localScale;
        float timer = 0;
        while (timer < despawnTime)
        {
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, timer / despawnTime);
            yield return null;
        }
        */

        yield return new WaitForSeconds(despawnTime);
        for (int i = 0; i < wallPieces.Length; i++)
        {
            wallPieces[i].SetActive(false);
        }
    }
}
