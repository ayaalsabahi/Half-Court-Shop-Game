using System.Collections;
using System.Collections.Generic;
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
    
    public GameObject thingToThrow;

    //Points
    [SerializeField]
    public int points;




    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + .2f, theGround);
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
            StartThrow();
        }
        if(isCharging)
        {
            Debug.Log("charge");
            ChargeThrow();
        }
        if(inHand != "" && Input.GetKeyUp(KeyCode.Mouse0))
        {
            Debug.Log("throw");
            ReleaseBall();
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
        moveDirection = (orientation.forward * verticalInput) + (orientation.right * horizontalInput);
        if(grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else{
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
    }
    
    private void ChargeThrow()
    {
        chargeTime += Time.deltaTime;
    }

    private void ReleaseBall()
    {
        ThrowObj(Mathf.Min(chargeTime * throwForce, maxForce));
        isCharging = false;
        inHand = "";
    }

    private void ThrowObj(float force)
    {
        Camera cam = GetComponent<PlayerInteract>().cam;

        Vector3 spawnPoint = throwPosition.position + cam.transform.forward;

        GameObject tossable = Instantiate(thingToThrow, spawnPoint, cam.transform.rotation);
        Debug.Log(tossable);
        Rigidbody itembody = tossable.GetComponent<Rigidbody>();
        Vector3 finalThrowDirection = (cam.transform.forward  + throwDirection).normalized;
        
        itembody.AddForce(finalThrowDirection * force, ForceMode.VelocityChange);

    }
}
