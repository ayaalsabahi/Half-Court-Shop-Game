using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private void Start()
    {
        if (GameController.S)
        {
            Destroy(GameController.S.gameObject);
        }
    }

    public void StartTheGame()
    {
        SceneManager.LoadScene("Kitchen");
    }

    public void btn_GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void btn_GoToScene(string thisScene)
    {
        SceneManager.LoadScene(thisScene);
    }

    public void btn_QuitTheGame()
    {
        Application.Quit();
    }
}



