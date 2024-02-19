using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

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

    //Hand texts
    [SerializeField]
    public String hand;
    public TMP_Text handText;

    private void Start()
    {
        handText.gameObject.SetActive(false);
    }
    

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
                //Debug.Log(interactable.promptMessage);
                if((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.E)) && interactable.name != "Oven")
                {
                    handText.gameObject.SetActive(true);
                    GameObject ingredient = hitInfo.collider.gameObject;
                    //interactable.BaseInteract();
                    // Debug.Log("pick up " + ingredient);
                    // Debug.Log("and the the tag is " + ingredient.tag);
                    player.GetComponent<PlayerController>().inHand = interactable.tag;
                    handText.text = "Current ingredient: " + interactable.tag;
                    Destroy(ingredient);
                }
                if((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.E)) && interactable.name == "Oven")
                {
                    GameObject ingredient = hitInfo.collider.gameObject;
                    interactable.BaseInteract();
                }
            }
        }


    }
}
