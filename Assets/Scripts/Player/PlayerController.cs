using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

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

    //Timer
    private float MAX_TIME = 62;
    private float timeElapsed;
    public TMP_Text timerText;

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

    [SerializeField]
    GameObject trajectoryEnd;


    //Points
    [SerializeField]
    public int score; 
    public TMP_Text scoreText; 

    [SerializeField]
    public int strikes;

    public TMP_Text strikeText;


    //music controls
    private AudioSource booSound;
    public AudioClip booClip;

    private AudioSource wooshSound;
    public AudioClip wooshClip;

    private AudioSource yaySound;
    public AudioClip yayClip;


    //Hand texts
    //[SerializeField]
    



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        //scoreText = GameObject.Find("Canvas").transform.GetChild(1).GetComponent<TMP_Text>();
        //strikeText = GameObject.Find("Canvas").transform.GetChild(2).GetComponent<TMP_Text>();
        cam = GetComponent<PlayerInteract>().cam;
        trajectoryEnd.SetActive(false);

        timeElapsed = 0.0f;

        //sound
        booSound = gameObject.AddComponent<AudioSource>();
        booSound.clip = booClip;
        booSound.loop = false;

        wooshSound = gameObject.AddComponent<AudioSource>();
        wooshSound.clip = wooshClip;
        wooshSound.loop = false;

        yaySound = gameObject.AddComponent<AudioSource>();
        yaySound.clip = yayClip;
        yaySound.loop = false;


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
            // trajectoryEnd.SetActive(true);
            ChargeThrow();
        }
        if(inHand != "" && Input.GetKeyUp(KeyCode.Mouse0))
        {
            // Debug.Log("throw");
            ReleaseBall();
        }

        timeElapsed += Time.deltaTime;
        if ( (MAX_TIME - timeElapsed) > 0)
        {
            UpdateUI();
        }
        else
        {
            // end game
            GameController.S.EndGame();
        }

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
        wooshSound.Play();
        trajectoryEnd.SetActive(false);
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
        GameObject tossable = Instantiate(thingToThrow, spawnPoint, Quaternion.identity); // Use Quaternion.identity for default rotation

        Rigidbody itemRb = tossable.GetComponent<Rigidbody>();
        Vector3 finalThrowDirection = (cam.transform.forward + throwDirection).normalized;

        // Calculate initial velocity based on force and mass, assuming ForceMode.VelocityChange
        Vector3 initialVelocity = finalThrowDirection * force / itemRb.mass;

        itemRb.AddForce(initialVelocity, ForceMode.VelocityChange); // Apply initial velocity as force
       
    }

    void ShowTrajectory(Vector3 origin, Vector3 initialVelocity)
    {
        Vector3[] points = new Vector3[100]; // Adjust the number of points as needed
        trajectoryLine.positionCount = points.Length;
        bool targetHit = false; // To track if we've hit a target object

        for (int i = 0; i < points.Length; i++)
        {
            float time = i * 0.1f; // Time step for each point (adjust as needed)
            Vector3 point = origin + initialVelocity * time + 0.5f * Physics.gravity * time * time;
            points[i] = point;

            if (!targetHit) // Perform raycast only if we haven't hit a target yet
            {
                RaycastHit hit;
                if (Physics.Raycast(point, Vector3.down, out hit, 1f)) // Adjust raycast length as needed
                {
                    // Update hit marker position and visibility
                    trajectoryEnd.transform.position = hit.point;
                    // trajectoryEnd.SetActive(true);

                    // Check for specific tag or layer
                    if (hit.collider.CompareTag("Pizza") || hit.collider.gameObject.layer == LayerMask.NameToLayer("Conveyor"))
                    {
                        targetHit = true; // Mark that we've hit a target
                        trajectoryLine.material.color = Color.green; // Change line color to green
                        trajectoryEnd.GetComponent<Renderer>().material.color = Color.green; // Change hit marker color to green
                    }
                }
            }

        }

        if (!targetHit) // If no target was hit, reset colors to default
        {
            trajectoryLine.material.color = Color.white; // Default color
            trajectoryEnd.GetComponent<Renderer>().material.color = Color.white; // Default color
        }

        trajectoryLine.SetPositions(points); // Update the trajectory line with calculated points
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

    public void IncrementPizzaBox()
    {
        Debug.Log("pizza should spawn");
        GameObject.FindGameObjectWithTag("PizzaBoxSpawner").GetComponent<PizzaBoxSpawnScript>().SpawnPizzaBox();
        yaySound.Play();
    }

    public void IncrementStrikes()
    {
        strikes += 1;
        booSound.Play();
    }

    private void UpdateUI()
    {
        timerText.text = Mathf.RoundToInt(MAX_TIME-timeElapsed).ToString();
        scoreText.text = "Score: " + score.ToString();
        strikeText.text = "Strikes: " + strikes.ToString();
    }
}
