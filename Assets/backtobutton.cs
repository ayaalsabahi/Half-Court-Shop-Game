using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backtobutton : MonoBehaviour
{
    // Start is called before the first frame update
    public void returnToHome()
    {
        SceneManager.LoadScene("homePage");
    }
}
