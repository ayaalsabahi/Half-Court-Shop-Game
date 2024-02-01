using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public Camera camera;
    // private Transform cameraLocation;


    [SerializeField]
    private float castDistance = 3f;
    // Start is called before the first frame update
    void Start()
    {
        // camera = GameObject.Find("CameraHolder").transform.GetChild(0).gameObject.Camera;
        // cameraLocation = this.gameObject.transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        // Ray ray = new Ray(cameraLocation.transform.position, cameraLocation.transform.forward);
        // Debug.DrawRay(ray.origin, ray.direction * castDistance);

    }
}
