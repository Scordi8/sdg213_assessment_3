using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIS_PauseMenu : MonoBehaviour
{
    /*GameIsPaused is a bool that dictates whether or not the game is paused
     * pauseMenuUI is intended for the Pause Menu UI
     * respawnUI is intended for the Respawn Menu UI
     * player is intended to be able to run the respawn script from the player
     * respawnTimer is used to count how long the player has held the respawn key
     * respawned is a bool that dictates whether or not a player has respawned since the last time the player pressed the respawn button
     * MainMenuScene is an int which is intended to fit whatever the main menu scene manager value is
    */
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public GameObject respawnUI;

    public Respawn player;

    float respawnTimer = 0f;

    bool respawned = false;

    public int MainMenuScene = 0;

    /*Every game update, 
     * if the F key is pressed, then it calls the askToRespawn function
     * if the F key is held for 2 seconds, then the game calls the UItoRespawn function
     * if the escape key is pressed, checks to see if the game is paused. If it is paused, it runs the Resume function. If it is not paused, it runs the Pause function.
     */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            respawned = false;
            respawnTimer = Time.realtimeSinceStartup;
            askToRespawn();
        }

        if (Input.GetKey(KeyCode.F))
        {
            if((Time.realtimeSinceStartup - respawnTimer > 2)& (respawned == false))
            {
                UItoRespawn();
            }
        }

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
        respawnUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    //askToRespawn function acts like the Pause function, but for the Respawn menu instead
    void askToRespawn()
    {
        respawnUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    //LoadMainMenu loads the scene number that is allocated to the MainMenuScene value
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MainMenuScene);
    }

    //When the QuitGame function is called, it quits the game
    public void QuitGame()
    {
        Application.Quit(); //NOTE: Only works in build, not in editor
    }

    //When the UItoRespawn function is called, says it has respawned, then call the toRespawn function from the player, and then runs the Resume function
    public void UItoRespawn()
    {
        respawned = true;
        player.toRespawn();
        Resume();
    }
}
