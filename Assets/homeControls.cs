using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class homeControls : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnMouseUpAsButton()
    {
        SceneManager.LoadScene("Kitchen");
    }
    
}
