using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    public PlayerController pc;

    public int score;
    public int strikes;
    // Start is called before the first frame update
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
