using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rb.velocity = new Vector3(0, 5, 0);
        }

        if(Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector3(0, 0, 5);
        }

        if(Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector3(0, 0, -5);
        }

        if(Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(-5, 0, 0);
        }

        if(Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(5, 0, 0);
        }
        
    }
}
