// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class HitMarkerController : MonoBehaviour
// {
//     public bool hitting;

//     void Start()
//     {
//         hitting = false;
//     }

//     void Update()
//     {
//         Debug.Log(hitting);
//     }

//     void OnTriggerEnter(Collider other)
//     {
//         if (other.gameObject.layer == LayerMask.NameToLayer("Conveyor") || other.CompareTag("Pizza"))
//         {
//             hitting = true;
//         }
//     }

//     void OnTriggerExit(Collider other)
//     {
//         if (other.gameObject.layer == LayerMask.NameToLayer("Conveyor") || other.CompareTag("Pizza"))
//         {
//             hitting = false;
//         }
//     }
// }

using UnityEngine;

public class HitMarkerController : MonoBehaviour
{
    public bool hitting;
    public float checkRadius = 0.5f; // Adjust the radius as needed to match the size of your hit marker

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius);
        hitting = false; // Reset hitting to false at the start of each Update call

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.layer == LayerMask.NameToLayer("Conveyor") || hitCollider.CompareTag("Pizza"))
            {
                hitting = true;
            }
        }
    }
}
