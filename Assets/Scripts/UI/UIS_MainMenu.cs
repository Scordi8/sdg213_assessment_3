using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIS_MainMenu : MonoBehaviour
{
    //level1 is the scene number for level 1, tempCurrentLevel is used to read and write the playerPref for currentLevel
    public int level1 = 0;
    public int tempCurrentLevel = 0;

    //At the start, set currentLevel to level1
    void Start()
    {
        PlayerPrefs.SetInt("currentLevel", level1);
    }

    //When the PlayGame function is called, gets the currentLevel and pushes the value to tempCurrentLevel, then loads whatever scene tempCurrentLevel has recieved, then ensure the game is running
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

    //When called, sets first checkpoint to start of level 1
    public void justStarted()
    {
        PlayerPrefs.SetFloat("checkpointX", -7f);
        PlayerPrefs.SetFloat("checkpointY", 0f);
        PlayerPrefs.SetFloat("checkpointZ", -16f);
    }
}
