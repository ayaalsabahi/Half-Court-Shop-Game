using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Camera camera;
    // private Transform cameraLocation;


    [SerializeField]
    private float castDistance = 3f;

    [SerializeField]
    private LayerMask mask;

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * castDistance);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, castDistance, mask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                Debug.Log(interactable.promptMessage);
                if(Input.GetMouseButtonDown(0) && interactable.name == "Pepperoni")
                {
                    // interactable.BaseInteract();
                    Debug.Log("touching Pepperoni");
                }
            }
        }


    }
}
