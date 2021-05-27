using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolePartPickup : MonoBehaviour
{
    public int amount;
    PoleBase pb;
    public GameObject Particle;
    bool horizontalPos;
    public bool leftSide;
    public bool randomSide;

    public float moveDuration;
    public float minDistance;
    public bool dynamic;
    public float dynamicMultiplier;
    public GameObject img;
    GameObject instImg;
    Canvas canv;

    Vector3 startPos;
    Vector3 targetPos;
    float offset;
    bool lerping;

    private void Start()
    {
        canv = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        if (transform.localEulerAngles.y == 0 || transform.localEulerAngles.y == 180 || transform.localEulerAngles.y == -180)
        {
            horizontalPos = true;
        }

        pb = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PoleBase>();
        startPos = transform.localPosition;
        if (randomSide)
        {
            if(Random.value > 0.5f)
            {
                leftSide = true;
            }
            else
            {
                leftSide = false;
            }
        }
        if (dynamic)
        {
            DynamicMovement(true);
        }
    }

    private void Update()
    {
        if (dynamic)
        {
            DynamicMovement(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PoleBase"))
        {
            other.GetComponent<PoleBase>().GrowPole(amount);
            Instantiate(Particle, transform.position, Quaternion.identity);
            StartCoroutine(Scale());
            GetComponent<Collider>().enabled = false;
            UiPickup();
        }
        if (other.CompareTag("Pole"))
        {
            pb = other.GetComponentInParent<PoleBase>();
            if (pb != null)
            {
                pb.GrowPole(amount);
                Instantiate(Particle, transform.position, Quaternion.identity);
                StartCoroutine(Scale());
                GetComponent<Collider>().enabled = false;
                UiPickup();
            }
        }
    }

    IEnumerator Scale()
    {
        float duration = 1f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, elapsed / duration);
            yield return null;
        }
        gameObject.SetActive(false);
    }

    IEnumerator LerpToTarget()
    {
        Vector3 currentPos = transform.localPosition;
        float duration = moveDuration;
        float elapsed = 0f;
        Vector3 tempTarget = targetPos;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(currentPos, tempTarget, elapsed / duration);
            yield return null;
        }
        transform.localPosition = tempTarget;
        lerping = false;
        yield return null;
    }
    public void UiPickup()
    {
        Vector3 namepos = Camera.main.WorldToScreenPoint(transform.position);
        instImg = Instantiate(img, Vector3.zero, Quaternion.identity);
        instImg.transform.SetParent(canv.transform, false);
        instImg.transform.position = namepos;
    }

    private void DynamicMovement(bool start)
    {

        targetPos = startPos;
        if (start)
        {
            if (pb.startSize < 12)
            {
                offset = 0.4375f * pb.startSize * dynamicMultiplier;
            }
            else
            {
                offset = 5.25f * dynamicMultiplier;
            }
        }
        else
        {
            if (pb.size < 12)
            {
                offset = 0.4375f * pb.size * dynamicMultiplier;
            }
            else
            {
                offset = 5.25f * dynamicMultiplier;
            }   
        }

        if (leftSide)
        {
            offset = -offset;
        }

        if (horizontalPos)
        {
            targetPos.x += offset;
        }
        else
        {
            targetPos.z -= offset;
        }
        if (start)
        {
            transform.localPosition = targetPos;
        }
        else
        {
            if (Vector3.Distance(pb.transform.position, transform.position) > minDistance)
            {
                if (transform.localPosition != targetPos && !lerping)
                {
                    lerping = true;
                    StartCoroutine("LerpToTarget");
                }
            }
            else if (lerping)
            {
                StopCoroutine("LerpToTarget");
                lerping = false;
            }
        }
    }

}
