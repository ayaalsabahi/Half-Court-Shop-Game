using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using System;

// CODE SOURCE: FROM Jayometric ON YOUTUBE
// https://www.youtube.com/watch?v=cSEg7Xm4A9A&ab_channel=Jayometric


public class ConveyorBelt : MonoBehaviour
{
    [SerializeField]
    private float speed, conveyorSpeed;
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private List<GameObject> onBelt;

    private Material material;

    [SerializeField]
    public TicketHolder ticketHolder;
    public Dictionary<int, List<string>> tickets;

    public List<int> toRemove;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        tickets = ticketHolder.GetComponent<TicketHolder>().tickets;
        CheckTickets();
    }

    // Fixed update for physics
    void FixedUpdate()
    {
        // For every item on the belt, add force to it in the direction given
        for (int i = 0; i <= onBelt.Count - 1; i++)
        {
            onBelt[i].GetComponent<Rigidbody>().AddForce(speed * direction);
            //Debug.Log(onBelt[i].GetComponent<Rigidbody>());
        }
    }

    // When something collides with the belt
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Pizza")
        {
            onBelt.Add(collision.gameObject);
        }
    }

    // When something leaves the belt
    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
    }

    public void CheckTickets()
    {
        foreach(int key in tickets.Keys)
        {
            // Debug.Log(tickets);
            // Debug.Log("ticket list is " + tickets[key]);
            // Debug.Log(1);
            // Debug.Log(String.Join("\n", tickets[key])); 
            if(CheckTicket(key))
            {
                return;
            }
        }
    }

    public bool CheckTicket(int key)
    {
         for(int j = 0; j < onBelt.Count; j++)
        {
            // Debug.Log("pizza list is" + onBelt[j].GetComponent<PizzaController>().IngredientsList);
            // Debug.Log(2);
            // Debug.Log(String.Join("\n", onBelt[j].GetComponent<PizzaController>().IngredientsList)); 
            if (tickets[key] == onBelt[j].GetComponent<PizzaController>().IngredientsList)
            //Ticket[i] matches pizza[j] on belt
            {
                Debug.Log("key match");
                ticketHolder.GetComponent<TicketHolder>().tickets.Remove(key);
                //Delete ticket

                Destroy(onBelt[j]);
                //Delete pizza
                return true;
            }
        }
        return false;
    }
}