using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrayController : MonoBehaviour
{
    PlayerController playerController;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pepperoni")
        {
            Debug.Log("points before " + playerController.points);
            playerController.points += 1;
            Debug.Log("points after " + playerController.points);
            
        }
    }
}
