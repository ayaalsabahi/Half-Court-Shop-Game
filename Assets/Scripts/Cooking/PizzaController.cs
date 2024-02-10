using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PizzaController : MonoBehaviour
{
    [SerializeField]
    public Material pepperoniPizza;
    Renderer meshRenderer;

    public Dictionary<string, int> ingredientsList = new Dictionary<string, int>();
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<Renderer>();
        ingredientsList.Add("Pepperoni", 0);
        ingredientsList.Add("Mushroom", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pepperoni")
        {
            // Debug.Log("making pepperoni");
            meshRenderer.material = pepperoniPizza;
        }
    }

    public void findIngredientToIncrease(string tag)
    {
        bool found = false;

        foreach(string key in ingredientsList.Keys)
        {
            if(key == tag)
            {
                found = true;
            } 
        }
        if (found)
        {
            ingredientsList[tag] = ingredientsList[tag]+1;
        }
        found = false;
    }
}


