using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorialScript : MonoBehaviour
{
    public void OnMouseUpAsButton()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
