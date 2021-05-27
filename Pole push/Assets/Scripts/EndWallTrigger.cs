using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndWallTrigger : MonoBehaviour
{
    PoleBase pb;
    EndWall ew;
    public int breakForce;
    FinishMain fm;
    GameObject tempPart;
    public bool wall9x;

    FinishTrigger[] triggers; 

    private void Start()
    {
        ew = transform.parent.GetComponent<EndWall>();
        fm = GetComponentInParent<FinishMain>();
        triggers = transform.GetComponentInParent<FinishMain>().GetComponentsInChildren<FinishTrigger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pole"))
        {
            if (transform.parent != null)
            {
                if (wall9x)
                {
                    tempPart = other.GetComponentInParent<PoleBase>().BreakMultiple(breakForce*100);
                    for (int i = 0; i < triggers.Length; i++)
                    {
                        if (!triggers[i].endFinish)
                        {
                            triggers[i].gameObject.SetActive(false);
                        }
                    }
                }
                else
                {
                    tempPart = other.GetComponentInParent<PoleBase>().BreakMultiple(breakForce);
                }
                ew.DestroyWall(tempPart);
                fm.multiplier += 1;
            }
        }
    }
}
