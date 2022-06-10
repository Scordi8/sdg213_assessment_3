using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    //GameState dictates what condition the game is in. If the gameState is 0, it means the game is act, 1 means the player has won, and 2 means the player has lost
    int gameState = 0;
    int tempCurrentLevel = 0;
    int loseLevel = 0;
    public UIS_MainMenu selfCanvas;

    /* At the start of the game
     * calls the OnGameStateChange function (to ensure game initially functions as intended)
    */
    void Start()
    {
        OnGameStateChange();
    }

    //When the toActive function is called, sets the game state to active and calls the OnGameStateChange
    public void toActive()
    {
        gameState = 0;
        OnGameStateChange();
    }

    //When toWin is called, gets the current level in a temp value, adds 1 to the temp value, sets the game state to win, calls OnGameStateChange
    public void toWin()
    {
        tempCurrentLevel = PlayerPrefs.GetInt("currentLevel");
        tempCurrentLevel++;
        gameState = 1;
        OnGameStateChange();
    }

    //When toLose is called, sets the game state to lose and calls the OnGameStateChange
    public void toLose()
    {
        gameState = 2;
        OnGameStateChange();
    }

    /* When called, creates actions based on what the game state is, these can have things added in future development
     * If active is currently empty
     * if won, sets the currentLevel to the temp value, then calls for PlayGame
     * If lost, calls lose screen
     * */
    public void OnGameStateChange()
    {
        if (gameState == 0)
        {

        }

        if (gameState == 1)
        {
            PlayerPrefs.SetInt("currentLevel", tempCurrentLevel);
            selfCanvas.PlayGame();
        }

        if (gameState == 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
    }
}
