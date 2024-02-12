using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    //Speed Modifiers
    public float moveSpeed;
    public float groundDrag;


    //Ground Mechanics
    public float playerHeight;
    public LayerMask theGround;
    bool grounded;

    //Jump Mechanincs
    public float jumpForce;
    public float airMultiplier;
    public float jumpCooldown;
    bool readyToJump = true;

    //Orientation
    public Transform orientation;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;


    //Holding and Throwing
    public string inHand = "";
    public bool isCharging = false;
    private float chargeTime = 0f;

    private float throwForce =5f;

    private float maxForce = 50f;
    private float maxTime = 50f;

    [SerializeField]
    private Transform throwPosition;
    private Vector3 throwDirection = new Vector3(0,1,0);
    
    [SerializeField]
    public GameObject[] throwableItems;
    public GameObject thingToThrow;

    bool easyModeOn = true;
    
    [SerializeField]
    private LineRenderer trajectoryLine;
    Camera cam;

    //Points
    [SerializeField]
    public int score;
    [SerializeField]
    TMP_Text scoreText; 

    [SerializeField]
    public int strikes;

    [SerializeField]
    TMP_Text strikeText;
    
    




    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        scoreText = GameObject.Find("Canvas").transform.GetChild(1).GetComponent<TMP_Text>();
        strikeText = GameObject.Find("Canvas").transform.GetChild(2).GetComponent<TMP_Text>();
        cam = GetComponent<PlayerInteract>().cam;
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + .7f, theGround);
        MyInput();
        SpeedControl();
        
        if(grounded)
        {
            rb.drag = groundDrag;
        }
        else{
            rb.drag = 0;
        }

        if(inHand != "" && Input.GetKeyDown(KeyCode.Mouse0))
        {
            FindThingToThrow();
            StartThrow();

        }
        if(isCharging)
        {
            // Debug.Log("charge");
            ChargeThrow();
            // if(easyModeOn)
            // {
            //     Vector3 ingredientVelocity = ();
            // }
        }
        if(inHand != "" && Input.GetKeyUp(KeyCode.Mouse0))
        {
            // Debug.Log("throw");
            ReleaseBall();
        }

        scoreText.text = "Score: " + score.ToString();
        strikeText.text = "Strikes: " + strikes.ToString();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if(Input.GetKey(KeyCode.Space) && grounded && readyToJump)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    //  private void MovePlayer()
    // {
    //     Vector3 horizontalMovement = orientation.right * horizontalInput;
    //     Vector3 verticalMovement = orientation.forward * verticalInput;

    //     Vector3 movement = (horizontalMovement + verticalMovement).normalized;

    //     if (grounded)
    //     {
    //         rb.AddForce(movement * moveSpeed * 10f, ForceMode.Force);
    //     }
    //     else
    //     {
    //         rb.AddForce(movement * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    //     }
    // }

    private void MovePlayer()
    {
        moveDirection = orientation.right * horizontalInput + orientation.forward * verticalInput;
        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void StartThrow()
    {
        isCharging = true;
        chargeTime = 0f;
        if(easyModeOn)
        {
            trajectoryLine.enabled = true;
        }
    }
    
    private void ChargeThrow()
    {
        chargeTime += Time.deltaTime;
        if(easyModeOn)
        {
            Vector3 itemVelocity = (cam.transform.forward + throwDirection).normalized * Mathf.Min(chargeTime * throwForce, maxForce);
            ShowTrajectory(throwPosition.position + throwPosition.forward, itemVelocity);
        }
    }

    private void ReleaseBall()
    {
        ThrowObj(Mathf.Min(chargeTime * throwForce, maxForce));
        isCharging = false;
        inHand = "";
         if(easyModeOn)
        {
            trajectoryLine.enabled = false;
        }
    }

    private void ThrowObj(float force)
    {
        Vector3 spawnPoint = throwPosition.position + cam.transform.forward;

        GameObject tossable = Instantiate(thingToThrow, spawnPoint, cam.transform.rotation);
        // Debug.Log(tossable);
        Rigidbody itembody = tossable.GetComponent<Rigidbody>();
        Vector3 finalThrowDirection = (cam.transform.forward  + throwDirection).normalized;
        
        itembody.AddForce(finalThrowDirection * force, ForceMode.VelocityChange);

    }

    private void ShowTrajectory(Vector3 origin, Vector3 velocity)
    {
        Vector3[] points = new Vector3[5000];
        trajectoryLine.positionCount = points.Length;
        for(int i = 0; i < points.Length; i++)
        {
            float time = i * 0.1f;
            points[i] = origin + velocity * time + .05f * Physics.gravity * time * time;
        }
        trajectoryLine.SetPositions(points);
    }

    private void FindThingToThrow()
    {
        for(int i = 0; i < throwableItems.Length; i++)
        {
            // Debug.Log("checking throw list for " + inHand);
            
            if (throwableItems[i].tag == inHand)
            {
                // Debug.Log("found it "+ throwableItems[i].tag);
                thingToThrow = throwableItems[i];
            }
        }
    }
}
