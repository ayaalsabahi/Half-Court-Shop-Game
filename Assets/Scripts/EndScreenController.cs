using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreenController : MonoBehaviour
{
    [SerializeField]
    GameObject scoreManager;

    [SerializeField]
    TMP_Text score;

    [SerializeField]
    TMP_Text strikes;

    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager");
        score.text = scoreManager.GetComponent<ScoreManager>().score.ToString();
        strikes.text = scoreManager.GetComponent<ScoreManager>().strikes.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
