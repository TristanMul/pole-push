using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject Particle;
    public GameObject img;
    GameObject instImg;
    Canvas canv;
    void Start()
    {
       canv = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PoleBase") || other.CompareTag("Pole"))
        {
            Instantiate(Particle, transform.position, Quaternion.identity);
            GetComponent<Collider>().enabled = false;
            StartCoroutine(Scale());
            UiPickup();
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

    public void UiPickup()
    {
        Vector3 namepos = Camera.main.WorldToScreenPoint(transform.position);
        instImg = Instantiate(img, Vector3.zero, Quaternion.identity);
        instImg.transform.SetParent(canv.transform, false);
        instImg.transform.position = namepos;
    }
}
