using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDeath : MonoBehaviour
{
    public CloseBridge bridge;
    public GameObject GameOverScreen;
    void OnTriggerEnter(Collider col)
    {
        if (bridge != null)
        {
            if (!bridge.hasClosed && col.CompareTag("PlayerTrigger") )
            {
                Die();
            }
        }
    }

    void Die()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var pole = GameObject.FindGameObjectWithTag("PoleBase");
        player.GetComponent<Movement>().speed = 0f;
        player.GetComponent<Movement>().anim.enabled = false;
        player.GetComponentInChildren<PoleBase>().PlayerDie();
        GameOverScreen.SetActive(true);
        StartCoroutine(Screen(GameOverScreen));
        Rigidbody[] list = player.transform.GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < list.Length; i++)
        {
            list[i].velocity = player.transform.forward * 2.5f;
        }
        Rigidbody[] list2 = pole.transform.GetComponentsInChildren<Rigidbody>();
        for (int i = 1; i < list2.Length; i++)
        {
            Destroy(list2[i].transform.GetComponentInChildren<Rigidbody>());
            print("disable pole rigidbodies");
        }
    }

    public IEnumerator Screen(GameObject go)
    {
        float counter = 0f;
        while (counter < 1f)
        {
            counter += 0.05f;
            yield return new WaitForSeconds(0.05f);
            go.GetComponent<CanvasGroup>().alpha = counter;
        }
        yield return null;
    }
}
