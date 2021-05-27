using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public static int totalLevels = 4;

    private void Start()
    {
        //Disable at launch!!!
        //PlayerPrefs.DeleteAll();

        if (!PlayerPrefs.HasKey("level"))
            SceneManager.LoadScene("0");

        if (!PlayerPrefs.HasKey("leveltext"))
            PlayerPrefs.SetInt("leveltext", 0);

        if (!PlayerPrefs.HasKey("gems"))
            PlayerPrefs.SetInt("gems", 0);

        if (PlayerPrefs.GetInt("level") > totalLevels)
            PlayerPrefs.SetInt("level", 1);

        SceneManager.LoadScene(PlayerPrefs.GetInt("level").ToString());
    }

    public void LoadScene()
    {
        if (PlayerPrefs.GetInt("level") > totalLevels)
            PlayerPrefs.SetInt("level", 1);

        SceneManager.LoadScene(PlayerPrefs.GetInt("level").ToString());
    }
}
