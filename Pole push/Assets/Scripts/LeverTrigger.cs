using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverTrigger : MonoBehaviour
{
    [Header("Which side is the trigger")]
    public bool isFrontSide;
    public LeverAnimation la;
    public CloseBridge leftBridge;
    public CloseBridge rightBridge;
    public BoxCollider otherTriggerCollider;
    BoxCollider triggerCollider;
    public GameObject deathTrigger;
    public ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        //Get the boxcollider component
        triggerCollider = GetComponent<BoxCollider>();

        //Turn off renderer of trigger
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //If the other object is the pole
        if (other.CompareTag("Pole"))
        {
            //Disable trigger on both sides
            triggerCollider.enabled = false;
            otherTriggerCollider.enabled = false;
            deathTrigger.SetActive(false);

            //Break the pole at the part that touched the switch
            //other.GetComponent<PolePart>().BreakPole();

            //Read which side the trigger is on to make the lever fall the right way
            if (isFrontSide)
            {
                //Make the lever fall forwards
                la.FallForward();
            }
            else
            {
                //Make the lever fall backwards
                la.FallBackward();
            }

            //Close both sides of the bridge
            leftBridge.Close();
            rightBridge.Close();

            var em = particles.emission;
            em.rateOverTime = 0f;
        }
    }
}
