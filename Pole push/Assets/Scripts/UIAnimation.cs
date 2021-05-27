using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    float speed = 1000f;
    GemAmount gemAmount;
    RectTransform rect;
    RectTransform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("GemGoal").GetComponent<RectTransform>();
        gemAmount = GameObject.FindGameObjectWithTag("GemAmount").GetComponent<GemAmount>();
        rect = gameObject.GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        float step = speed * Time.deltaTime;
        Vector3 pos = new Vector3(target.localPosition.x - target.rect.width, target.localPosition.y - target.rect.height, target.localPosition.z);
        rect.localPosition = Vector3.MoveTowards(rect.localPosition, pos, step);

        if (Vector3.Distance(rect.localPosition, pos) < 10f)
        {
            gemAmount.gems++;
            gemAmount.levelGems++;
            Destroy(gameObject);
        }
    }
}
