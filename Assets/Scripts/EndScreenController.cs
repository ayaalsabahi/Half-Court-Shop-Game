using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreenController : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    TMP_Text score;

    [SerializeField]
    TMP_Text strikes;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        score.text = pc.score.ToString();
        strikes.text = pc.strikes.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
