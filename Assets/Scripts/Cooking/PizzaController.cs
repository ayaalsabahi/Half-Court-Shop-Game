using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaController : MonoBehaviour
{
    [SerializeField]
    public Material pepperoniPizza;
    Renderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pepperoni")
        {
            Debug.Log("making pepperoni");
            meshRenderer.material = pepperoniPizza;
        }
    }
}
