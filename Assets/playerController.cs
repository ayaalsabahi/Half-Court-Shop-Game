using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//some notes: using collider will be hard to check if we are using mouse in order to 'throw' the ball
//rather, use getMouseDown
public class playerController : MonoBehaviour
{

    private bool isHolding = false;
    private GameObject heldObject;
    private Vector3 initialBallPosition;
    public float throwForce = 5f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Ball"))
                {
                    isHolding = true;
                    heldObject = hit.collider.gameObject;
                    initialBallPosition = heldObject.transform.position;
                    Debug.Log("Collided with ball");
                }
            }
        }

        if (isHolding && Input.GetMouseButtonUp(0))
        {
            isHolding = false;

            // Get the direction based on mouse position
            Vector3 throwDirection = CalculateThrowDirection();

            // Release the ball
            heldObject.GetComponent<Rigidbody>().isKinematic = false;
            heldObject.transform.SetParent(null);

            // Apply force to the ball in the calculated direction
            heldObject.GetComponent<Rigidbody>().AddForce(throwDirection * throwForce, ForceMode.Impulse);

            // Reset the heldObject reference
            heldObject = null;
        }
    }

    Vector3 CalculateThrowDirection()
    {
        // Calculate the throw direction based on the difference between mouse and initial ball positions
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        return (mouseWorldPosition - initialBallPosition).normalized;
    }



}
