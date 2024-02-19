using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.SceneManagement;

using TMPro;


public enum GameState { Menu, InKitchen, GameOver };



public class GameController : MonoBehaviour
{

    public static GameController S; // define the singleton

    public GameState currentState;

    public PlayerController playerScript;

    public TMP_Text msgText;

    //public ticking;

    public GameObject tutorial;

    public GameObject ticketCanvas;

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
        //DontDestroyOnLoad(this);

        msgText = GameObject.FindGameObjectWithTag("MessageText").GetComponent<TMP_Text>();
        Time.timeScale = 0;


        StartCoroutine(ReadySetText());
    }

    //private void CheckForWin()
    //{
    //    playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    //    if (playerScript.score < 10)
    //    {
    //        currentState = GameState.InKitchen;
    //    }
    //    else
    //    {
    //        currentState = GameState.GameOver;
    //        EndGame();
    //    }


    //}

    public void Update()
    {

        if (Input.GetKeyUp(KeyCode.E))
        {
            tutorial.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void EndGame()
    {
        // you win!
        SceneManager.LoadScene("WinScene");
    }

    public void GoToKitchen()
    {
        GameController.S.currentState = GameState.InKitchen;
    }

    private IEnumerator ReadySetText()
    {
        msgText.text = "Get ready...";

        yield return new WaitForSeconds(1.5f);

        StartCoroutine(GoText());
    }

    private IEnumerator GoText()
    {
        msgText.text = "THROW!";
        yield return new WaitForSeconds(1.0f);
        msgText.enabled = false;
        Canvas ticketCanvas  = GameObject.FindGameObjectWithTag("TicketCanvas").GetComponent<Canvas>();
        ticketCanvas.enabled = true;
    }
}
