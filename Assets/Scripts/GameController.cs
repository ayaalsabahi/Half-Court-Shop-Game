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

    public GameObject pizzaPrefab;


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
        GameController.S.currentState = GameState.Menu;
        DontDestroyOnLoad(this);
    }

    public void SpawnOrder()
    {
        GenerateNewOrder();
        CreatePizza();
    }

    private void CreatePizza()
    {
        Instantiate(pizzaPrefab);
    }

    private void GenerateNewOrder()
    {

    }

    public void GoToKitchen()
    {
        GameController.S.currentState = GameState.InKitchen;
    }
}
