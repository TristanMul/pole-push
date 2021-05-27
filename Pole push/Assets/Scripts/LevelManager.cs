using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
        
    public void NextLevelButtonClick()
    {
        Movement.startGame = false;

        var currentLevel = PlayerPrefs.GetInt("level");
        PlayerPrefs.SetInt("level", currentLevel+1);

        var levelText = PlayerPrefs.GetInt("leveltext");
        PlayerPrefs.SetInt("leveltext", levelText + 1);

        if (PlayerPrefs.GetInt("level") > Loader.totalLevels) { PlayerPrefs.SetInt("level", 1); }

        SceneManager.LoadScene(PlayerPrefs.GetInt("level").ToString());
    }
    public void OnRestarButtonClick()
    {
        Movement.startGame = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name.ToString());
    }
}
