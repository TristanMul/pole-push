using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCol : MonoBehaviour
{
    public Animator anim;
    public Movement move;
    public GameObject poleBase;
    public GameObject GameOverScreen;
    public GameObject CompleteScreen;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Bad"))
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            move.speed = 0;
            anim.enabled = false;
            poleBase.SetActive(false);
            GameOverScreen.SetActive(true);
            StartCoroutine(Screen(GameOverScreen));
            Rigidbody[] list = player.transform.GetComponentsInChildren<Rigidbody>();
            for (int i = 0; i < list.Length; i++)
            {
                list[i].velocity = Vector3.forward;
            }
        }
        //if (col.CompareTag("Finish"))
        //{
        //    move.speed = 0;
        //    anim.Play("Victory");
        //    anim.SetLayerWeight(1, 0f);
        //    CompleteScreen.SetActive(true);
        //    StartCoroutine(Screen(CompleteScreen));
        //}
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
