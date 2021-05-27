using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishTrigger : MonoBehaviour
{
    public int minLength;
    public bool endFinish;

    PoleBase pb;
    FinishMain fm;
    PlayerCol player;
    Text totalGems;
    Text multiplierText;
    GemAmount currentGems;

    public GameObject confetti1;
    public GameObject confetti2;

    public GameObject ground;
    Material groundMat;
    Color startColor;
    Color targetColor = Color.white;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerTrigger").GetComponent<PlayerCol>();
        currentGems = GameObject.FindGameObjectWithTag("GemAmount").GetComponent<GemAmount>();
        GetComponent<MeshRenderer>().enabled = false;
        fm = GetComponentInParent<FinishMain>();

        groundMat = ground.GetComponent<Renderer>().material;
        startColor = groundMat.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PoleBase"))
        {
            pb = other.GetComponent<PoleBase>();
            if (pb.size <= 0 || endFinish)
            {
                player = pb.transform.parent.GetComponentInChildren<PlayerCol>();

                confetti1.SetActive(true);
                confetti2.SetActive(true);

                StartCoroutine("BlinkGround");

                pb.transform.parent.GetComponent<Movement>().speed = 0;
                pb.transform.GetComponentInParent<Animator>().Play("Victory");

                player.CompleteScreen.SetActive(true);
                StartCoroutine(player.Screen(player.CompleteScreen));

                totalGems = GameObject.FindGameObjectWithTag("TotalGems").GetComponent<Text>();
                int obtainedGems = currentGems.levelGems * fm.multiplier;
                totalGems.text = obtainedGems.ToString();
                var currentAmount = PlayerPrefs.GetInt("gems");
                PlayerPrefs.SetInt("gems", currentAmount + obtainedGems);
                multiplierText = GameObject.FindGameObjectWithTag("MultiplierText").GetComponent<Text>();
                multiplierText.text = fm.multiplier.ToString() + "X";
                //Debug.Log("Gem multiplier is: " + fm.multiplier);
            }
        }
    }

    //This coroutine makes the ground blink 3 times
    IEnumerator BlinkGround()
    {
        //Time and count based variables
        float blinkTime = 1;
        float timer = 0;
        int blinkCounter = 0;
        
        //This whileloop create the Update for all the blinking
        while (timer < blinkTime)
        {
            //This while loop lerps from startcolor to white
            while (timer < blinkTime)
            {
                //Keep track of time
                timer += Time.deltaTime;
                //If the ground material was found lerp the color to white
                if (groundMat != null)
                {
                    groundMat.color = Color.Lerp(startColor, targetColor, timer / blinkTime);
                }
                //Wait a frame to simulate update
                yield return null;
            }
            //Set the color to white
            groundMat.color = targetColor;

            //Reset the timer
            timer = 0;

            //This while loop lerps from white to startcolor 
            while (timer < blinkTime)
            {
                //Keep track of time
                timer += Time.deltaTime;
                //If the ground material was found lerp the color to startcolor
                if (groundMat != null)
                {
                    groundMat.color = Color.Lerp(targetColor, startColor, timer / blinkTime);
                }
                //Wait a frame to simulate update
                yield return null;
            }
            //Set the color to startcolor
            groundMat.color = startColor;

            //After the pingpong lerp check the counter
            if (timer >= blinkTime && blinkCounter < 2)
            {
                //If the counter hasn't reached 2 yet reset the timer to start from the begin of this while loop
                timer = 0;
                blinkCounter++;
            }
            //Wait a frame to simulate update
            yield return null;
        }
        //Wait a frame to simulate update
        yield return null;
    }
}
