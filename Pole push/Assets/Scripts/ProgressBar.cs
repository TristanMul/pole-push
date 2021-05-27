using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;
    public Text levelText;
    Movement move;
    public float levelDistance = 0f;
    float sliderValue;

    // Start is called before the first frame update
    void Start()
    {
        move = FindObjectOfType<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (levelDistance == 0f)
        {
            sliderValue = move.distance / 300f;
        }
        else
        {
            sliderValue = move.distance / levelDistance;
        }
        slider.value = sliderValue;
    }
}
