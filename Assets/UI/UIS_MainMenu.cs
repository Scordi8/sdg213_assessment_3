using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIS_MainMenu : MonoBehaviour
{
    //When the PlayGame function is called, move to the next level, then ensure the game is running
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }

    //When the QuitGame function is called, make a debug message saying it was called, then quit the game
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit(); //NOTE: Only works in build, not in editor
    }
}
