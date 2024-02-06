using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CODE SOURCE: FROM Jayometric ON YOUTUBE
// https://www.youtube.com/watch?v=cSEg7Xm4A9A&ab_channel=Jayometric


public class ConveyorBelt : MonoBehaviour
{
    [SerializeField]
    private float speed, conveyorSpeed;
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private List<GameObject> onBelt;

    private Material material;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {

    }

    // Fixed update for physics
    void FixedUpdate()
    {
        // For every item on the belt, add force to it in the direction given
        for (int i = 0; i <= onBelt.Count - 1; i++)
        {
            onBelt[i].GetComponent<Rigidbody>().AddForce(speed * direction);
            Debug.Log(onBelt[i].GetComponent<Rigidbody>());
        }
    }

    // When something collides with the belt
    private void OnCollisionEnter(Collision collision)
    {
        onBelt.Add(collision.gameObject);
    }

    // When something leaves the belt
    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
    }
}