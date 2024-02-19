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
    // Start is called before the first frame update

    private void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();


        DontDestroyOnLoad(this.gameObject);
        //if (currentScene.name == "WinScene")
        //{
        //}
        //else { Destroy(this.gameObject); }
    }

    void Start()
    {
        // PlayerController pc = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        score = pc.score;
        strikes = pc.strikes; 
    }
}
