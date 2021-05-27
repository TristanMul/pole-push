using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemAmount : MonoBehaviour
{
    [HideInInspector]
    public int gems;
    public int levelGems;

    void Start()
    {
        GetComponent<Text>().text = PlayerPrefs.GetInt("gems").ToString();
        gems = PlayerPrefs.GetInt("gems");
    }
    void FixedUpdate()
    {
        GetComponent<Text>().text = gems.ToString();
    }
}
