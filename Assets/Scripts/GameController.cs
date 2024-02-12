using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.SceneManagement;


public enum GameState { Menu, InKitchen, GameOver };



public class GameController : MonoBehaviour
{

    public static GameController S; // define the singleton

    public GameState currentState;

    public PlayerController playerScript;


    //winner starts at being still playing
    private void Awake()
    {
        if (GameController.S)
        {
            // the game manager already exists, destroy myself
            Destroy(this.gameObject);
        }
        else
        {
            S = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameController.S.currentState = GameState.InKitchen;
        DontDestroyOnLoad(this);
    }

    private void CheckForWin()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (playerScript.score < 10)
        {
            currentState = GameState.InKitchen;
        }
        else
        {
            currentState = GameState.GameOver;
            EndGame();
        }
    }

    public void EndGame()
    {
        // you win!
    }

    public void GoToKitchen()
    {
        GameController.S.currentState = GameState.InKitchen;
    }
}
