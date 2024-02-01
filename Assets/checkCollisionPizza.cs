using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkCollisionPizza : MonoBehaviour
{
    // Start is called before the first frame update

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("Ball hit me!");
            //possibly destroy that ball object & make it appear in the pizza somewhere? 

        }
    }
}
