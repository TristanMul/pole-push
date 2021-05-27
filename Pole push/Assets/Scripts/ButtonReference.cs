using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReference : MonoBehaviour
{
    public List<GameObject> objectList = new List<GameObject>();
    
    public float speed;
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("PlayerTrigger"))
        {
            for (int i = 0; i < objectList.Count; i++)
            {
                objectList[i].GetComponent<Cutter>().activated = true;
            }
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        float duration = 0.25f;
        float elapsed = 0f;
        float targetPos = transform.localPosition.y - 0.2f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, targetPos, transform.localPosition.z), elapsed / duration);
            yield return null;
        }
    }
}
