using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Camera cam;
    // private Transform cameraLocation;


    [SerializeField]
    private float castDistance = 3f;

    [SerializeField]
    private LayerMask mask;

    [SerializeField]
    private GameObject player;

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * castDistance);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, castDistance, mask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                Debug.Log(interactable.promptMessage);
                if((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.E)) && interactable.name == "Pepperoni")
                {
                    GameObject ingredient = hitInfo.collider.gameObject;
                    //interactable.BaseInteract();
                    Debug.Log("touching " + ingredient);
                    player.GetComponent<PlayerController>().inHand = interactable.name;
                    Destroy(ingredient);
                }
            }
        }


    }
}
