using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIS_PauseMenu : MonoBehaviour
{
    /*First value is a bool that dictates whether or not the game is paused
     * Second value is intended for the Pause Menu UI
     * Third value is an int which is intended to fit whatever the main menu scene manager value is
    */
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public int MainMenuScene = 0;

    //Every game update, if the escape key is pressed, checks to see if the game is paused. If it is paused, it runs the Resume function. If it is not paused, it runs the Pause function.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    /* Resume and Pause functions are very similar. 
     * Sets whether or not the Pause Menu is visible and the GameIsPaused value to true or false (true/visible in Pause, false/invisible in Resume), sets the time scale to normal (1f) in Resume and stopped (0f) in Pause 
     */
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    //LoadMainMenu first makes a debug log message saying MainMenu, then loads the scene number that is allocated to the MainMenuScene value
    public void LoadMainMenu()
    {
        Debug.Log("MainMenu");
        SceneManager.LoadScene(MainMenuScene);
    }

    //When the QuitGame function is called, make a debug message saying it was called, then quit the game
    public void QuitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit(); //NOTE: Only works in build, not in editor
    }
}
