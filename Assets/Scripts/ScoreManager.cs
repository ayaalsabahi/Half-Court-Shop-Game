using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    public PlayerController pc;

    public int score;
    public int strikes;

    public  static ScoreManager S;
    // Start is called before the first frame update

    private void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        GameObject scoreMan = GameObject.FindWithTag("ScoreManager");
        DontDestroyOnLoad(this.gameObject);

        if (ScoreManager.S)
            Destroy(this.gameObject);
        else
            ScoreManager.S = this;
        //if (currentScene.name == "WinScene")
        //{
        //}
        //else { Destroy(this.gameObject); }
    }

    void Start()
    {
         PlayerController pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pc)
            pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        score = pc.score;
        strikes = pc.strikes; 
    }
}
