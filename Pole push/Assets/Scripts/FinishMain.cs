using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishMain : MonoBehaviour
{
    public bool overWriteWalls;
    public int wallStrenght;
    public int multiplier;

    FinishTrigger[] ft;
    EndWallTrigger[] ewt;


    // Start is called before the first frame update
    void Start()
    {
        multiplier = 1;
        if (overWriteWalls)
        {
            ft = transform.GetComponentsInChildren<FinishTrigger>();
            ewt = transform.GetComponentsInChildren<EndWallTrigger>();
            for (int i = 0; i < ft.Length; i++)
            {
                ft[i].minLength = wallStrenght;
            }
            for (int i = 0; i < ewt.Length; i++)
            {
                ewt[i].breakForce = wallStrenght;
            }
        }
    }
}
