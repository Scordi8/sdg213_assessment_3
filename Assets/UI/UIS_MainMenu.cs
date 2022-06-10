using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIS_MainMenu : MonoBehaviour
{
    public int level1 = 0;
    public int tempCurrentLevel = 0;
    void Start()
    {
        PlayerPrefs.SetInt("currentLevel", level1);
    }

    //When the PlayGame function is called, move to the next level, then ensure the game is running
    public void PlayGame()
    {
        tempCurrentLevel = PlayerPrefs.GetInt("currentLevel");
        SceneManager.LoadScene(tempCurrentLevel);
        Time.timeScale = 1f;
    }

    //When the QuitGame function is called, make a debug message saying it was called, then quit the game
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit(); //NOTE: Only works in build, not in editor
    }

    public void justStarted()
    {
        PlayerPrefs.SetFloat("checkpointX", -7f);
        PlayerPrefs.SetFloat("checkpointY", 0f);
        PlayerPrefs.SetFloat("checkpointZ", -16f);
    }
}
