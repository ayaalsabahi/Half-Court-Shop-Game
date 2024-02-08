using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pizzaMovement : MonoBehaviour
{
    public float amplitude = 1.0f; // The amount of vertical movement
    public float speed = 1.0f; // The speed of the movement

    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
