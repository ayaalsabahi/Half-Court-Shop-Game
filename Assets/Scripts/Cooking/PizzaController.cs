using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PizzaController : MonoBehaviour
{
    [SerializeField]
    public Material pepperoniPizza;
    Renderer meshRenderer;

    public Dictionary<string, int> ingredientsDict = new Dictionary<string, int>();

    public List<string> IngredientsList;

    public int pointsToAdd;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<Renderer>();
        ingredientsDict.Add("Pepperoni", 0);
        ingredientsDict.Add("Mushroom", 0);
        ingredientsDict.Add("Olive", 0);
        ingredientsDict.Add("Pineapple", 0);
    }

    // Update is called once per frame
    void Update()
    {
        // if(ingredientsDict["Pepperoni"] > 0 && ingredientsDict["Mushroom"] > 0)
        // {
        //     Debug.Log("pizza done");
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pepperoni")
        {
            // Debug.Log("making pepperoni");
            meshRenderer.material = pepperoniPizza;
        }

        if (other.GetComponent<IngredientController>() != null)
        {
            pointsToAdd+=1;
        }

    }

    public void findIngredientToIncrease(string tag)
    {
        bool found = false;

        foreach(string key in ingredientsDict.Keys)
        {
            if(key == tag)
            {
                found = true;
            } 
        }
        if(found)
        {
            ingredientsDict[tag] = ingredientsDict[tag]+1;
            if(ingredientsDict[tag] == 1)
            {
                IngredientsList.Add(tag);
            }
        }
        found = false;
    }
}


