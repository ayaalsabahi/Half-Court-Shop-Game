using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Rigidbody rb;

    // float horizontalInput;
    // float verticalInput;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     rb = GetComponent<Rigidbody>();
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     MyInput();
    //     if(Input.GetKey(KeyCode.Space))
    //     {
    //         rb.velocity = new Vector3(0, 5, 0);
    //     }

    //     if(Input.GetKey(KeyCode.W))
    //     {
    //         rb.velocity = new Vector3(0, 0, 5);
    //     }

    //     if(Input.GetKey(KeyCode.S))
    //     {
    //         rb.velocity = new Vector3(0, 0, -5);
    //     }

    //     if(Input.GetKey(KeyCode.A))
    //     {
    //         rb.velocity = new Vector3(-5, 0, 0);
    //     }

    //     if(Input.GetKey(KeyCode.D))
    //     {
    //         rb.velocity = new Vector3(5, 0, 0);
    //     }
        
    // }
   
    
    public float moveSpeed;
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        MyInput();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

     private void MovePlayer()
    {
        moveDirection = (orientation.forward * verticalInput) + (orientation.right * horizontalInput);
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }
}
