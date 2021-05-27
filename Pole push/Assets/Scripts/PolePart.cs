using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolePart : MonoBehaviour
{
    GameObject objectPool;
    PoleBase pb;
    GameObject previousPart;
    Rigidbody rb;
    BoxCollider bc;
    CapsuleCollider cc;
    Material mat;
    Vector3 direction;

    Color startColor;
    Color targetColor = Color.white;

    private float despawnWaitTime = 5;

    public bool pushWhileBreaking;
    public float breakForce;

    public float colorLerpDuration;


    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        startColor = mat.color;
        bc = GetComponent<BoxCollider>();
        cc = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
    }

    //This fuction breaks this part and all it's childern from its parent
    public void BreakPole(bool child)
    {
        //To check if it still is part of the pole and tell it what is the tip search for PoleBase
        //If the search returns null that means it is no longer part of the pole
        pb = GetComponentInParent<PoleBase>();
        //If it's not null
        if (pb != null)
        {
            //Tell the base that the part before this one is now the last part
            if (!child)
            {
                pb.SetLastPart(previousPart);
                pb.ReadSize();
            }

            if (transform.childCount > 0)
            {
                transform.GetChild(0).GetComponent<PolePart>().BreakPole(true);
            }
            bc.enabled = false;
            cc.enabled = true;
            gameObject.layer = 25;

            if (!child)
            {
                transform.parent = null;
            }
            else
            {
                Destroy(rb);
            }

            //Let this part (and all its children) fall
            rb.isKinematic = false;


            gameObject.GetComponent<Rigidbody>().AddExplosionForce(
                500 * gameObject.GetComponentInParent<Rigidbody>().mass * 2.5f,
                new Vector3(transform.position.x + Random.Range(-10, 10), transform.position.y - 1f, transform.position.z), 10);

            //Despawn the broken piece after a given time after breaking
            StartCoroutine("DespawnAfterTime");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bad"))
        {
            if (pushWhileBreaking)
            {
                pb = GetComponentInParent<PoleBase>();
                //If it's not null
                if (pb != null)
                {
                    direction = collision.gameObject.transform.position - collision.GetContact(0).point;
                    direction = direction.normalized;
                    collision.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(direction * breakForce, collision.GetContact(0).point);
                }
            }
            BreakPole(false);
        }
    }

    public void SetPreviousPart(GameObject part)
    {
        previousPart = part;
    }

    //Reset the pole and all of its childern back into the pool
    public void Despawn()
    {
        //If it has a child pole part call its despawn
        if (transform.childCount > 0)
        {
            transform.GetChild(0).GetComponent<PolePart>().Despawn();
        }
        //Put it back into the pool
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.mass = 0.0001f;
        }
        transform.parent = objectPool.transform;
        rb.isKinematic = true;
        bc.enabled = true;
        cc.enabled = false;
        gameObject.layer = 23;
        gameObject.SetActive(false);
    }

    //Tell the pole part what the objectpool gameobject is to return to in depawn
    public void SetObjectPool(GameObject pool)
    {
        objectPool = pool;
    }

    //Call the coroutine to grow the pole
    public void GrowPole()
    {
        StartCoroutine("GrowPoleCoroutine");
    }

    //This is a coroutine that lerps the pole part from inside the last pole part to in front of it
    IEnumerator GrowPoleCoroutine()
    {
        //Make the variables for the coroutine
        float elapsedTime = 0;
        float waitTime = 0.5f;

        //Using a while loop lerp the position
        while (elapsedTime < waitTime)
        {
            transform.localPosition = Vector3.Lerp(Vector3.zero, Vector3.up * 2, (elapsedTime / waitTime));
            //Keep track of time for the lerp and to exit the while loop
            elapsedTime += Time.deltaTime;
            //Yield to wait a frame before the coroutine continues
            yield return null;
        }
        //Make sure the part is at the target position
        transform.localPosition = Vector3.up * 2;
        yield return null;
    }

    //This si a coroutine that despawns the 
    IEnumerator DespawnAfterTime()
    {
        yield return new WaitForSeconds(despawnWaitTime);
        Despawn();
    }

    IEnumerator LerpChain()
    {
        StartCoroutine("LerpWhite");
        yield return new WaitForSeconds(0f);
        if (transform.childCount > 0)
        {
            transform.GetChild(0).GetComponent<PolePart>().StartCoroutine("LerpChain");
        }
    }

    IEnumerator LerpWhite()
    {
        float elapsedTime = 0;
        float waitTime = colorLerpDuration;

        //Using a while loop lerp the position
        while (elapsedTime < waitTime * 0.5f)
        {
            if (mat != null)
            {
                mat.color = Color.Lerp(startColor, targetColor, (elapsedTime / (waitTime * 0.5f)));
            }
            //Keep track of time for the lerp and to exit the while loop
            elapsedTime += Time.deltaTime;
            //Yield to wait a frame before the coroutine continues
            yield return null;
        }
        mat.color = targetColor;
        elapsedTime = 0;
        while (elapsedTime < waitTime * 0.5f)
        {
            mat.color = Color.Lerp(targetColor, startColor, (elapsedTime / (waitTime * 0.5f)));
            //Keep track of time for the lerp and to exit the while loop
            elapsedTime += Time.deltaTime;
            //Yield to wait a frame before the coroutine continues
            yield return null;
        }
        mat.color = startColor;
        yield return null;
    }

}
