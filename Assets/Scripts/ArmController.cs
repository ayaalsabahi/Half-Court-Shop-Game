using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    public float sensx;
    public float sensy;

    public Transform orientation;

    float xrotation;
    float yrotation;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensx;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensy;
        
        yrotation += mouseX;
        xrotation -= mouseY;
        xrotation = Mathf.Clamp(xrotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xrotation, yrotation, 0);
        orientation.rotation = Quaternion.Euler(0, yrotation, 0);
    }
}
