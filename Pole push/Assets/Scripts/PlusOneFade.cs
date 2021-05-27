using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusOneFade : MonoBehaviour
{
    float speed = 100f;
    RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
        rect = gameObject.GetComponent<RectTransform>();
        StartCoroutine(ScaleUp());
    }

    // Update is called once per frame
    void Update()
    {
        rect.localPosition += (Vector3.up*speed)* Time.deltaTime;
    }

    IEnumerator ScaleUp()
    {
        Vector3 startSize = transform.localScale;
        float duration = 0.5f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startSize, Vector3.one, elapsed / duration);
            yield return null;
        }
        StartCoroutine(ScaleDown());
    }
    IEnumerator ScaleDown()
    {
        Vector3 startSize = transform.localScale;
        float duration = 0.5f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startSize, Vector3.zero, elapsed / duration);
            yield return null;
        }
        Destroy(gameObject);
    }
}
