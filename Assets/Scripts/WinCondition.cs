using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            toWin();
        }
    }

    public void toActive()
    {
        gameState = 0;
        OnGameStateChange();
    }

    public void toWin()
    {
        tempCurrentLevel = PlayerPrefs.GetInt("currentLevel");
        tempCurrentLevel++;
        gameState = 1;
        OnGameStateChange();
    }

    public void toLose()
    {
        gameState = 2;
        OnGameStateChange();
    }

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
            PlayerPrefs.SetInt("currentLevel", tempCurrentLevel);
            selfCanvas.PlayGame();
        }
    }
}
