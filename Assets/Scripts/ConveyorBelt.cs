using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using System;
using System.Linq;

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

    private Vector3 MAX_FORCE;

    // Start is called before the first frame update
    void Start()
    {
        MAX_FORCE = new Vector3(10.0f, 10.0f, 10.0f);
    }

    // Update is called once per frame
    private void Update()
    {
        tickets = ticketHolder.GetComponent<TicketHolder>().tickets;
        //Debug.Log(onBelt.Count);
        if(onBelt.Count > 0)
        {
            CheckTickets();
        }
    }

    // Fixed update for physics
    void FixedUpdate()
    {
        // For every item on the belt, add force to it in the direction given
        for (int i = 0; i <= onBelt.Count - 1; i++)
        {
            //Debug.Log(onBelt[i].GetComponent<Rigidbody>().GetAccumulatedForce().z + "VS" + MAX_FORCE.z);
            if (onBelt[i].GetComponent<Rigidbody>().GetAccumulatedForce().z <= MAX_FORCE.z)
                onBelt[i].GetComponent<Rigidbody>().AddForce(speed * direction, ForceMode.Impulse);
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
            List<string> toCompare = onBelt[j].GetComponent<PizzaController>().IngredientsList;
            // Debug.Log(2);
            // Debug.Log(String.Join("\n", onBelt[j].GetComponent<PizzaController>().IngredientsList));
            if(tickets[key].All(toCompare.Contains) && tickets[key].Count == toCompare.Count)
            // if (tickets[key] == onBelt[j].GetComponent<PizzaController>().IngredientsList)
            //Ticket[i] matches pizza[j] on belt
            {
                Debug.Log("key match");

                ticketHolder.GetComponent<TicketHolder>().DeleteTicket(key);

                // Debug.Log($"Attempting to remove ticket with key: {key}");
                // Debug.Log($"Tickets before removal: {string.Join(", ", tickets.Keys)}");
                // bool removed = ticketHolder.GetComponent<TicketHolder>().tickets.Remove(key);
                // Debug.Log($"Removal successful: {removed}");
                // Debug.Log($"Tickets after removal: {string.Join(", ", tickets.Keys)}");




                // ticketHolder.GetComponent<TicketHolder>().tickets.Remove(key);
                // //Delete ticket

                GameObject toDestroy = onBelt[j];
                PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
                playerController.score += onBelt[j].GetComponent<PizzaController>().pointsToAdd;
                playerController.IncrementPizzaBox();
                onBelt.RemoveAt(j); // Remove the pizza from the list first
                Destroy(toDestroy);
                //Delete pizza
                return true;
            }
        }
        return false;
    }
}