using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class creditsButton : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        SceneManager.LoadScene("credits");
    }
}
