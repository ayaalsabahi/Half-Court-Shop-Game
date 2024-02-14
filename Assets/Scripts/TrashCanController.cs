using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Pizza")
        {
            GameObject.Find("Player").GetComponent<PlayerController>().IncrementStrikes();
            Destroy(collision.gameObject);
        }
    }
    
}
