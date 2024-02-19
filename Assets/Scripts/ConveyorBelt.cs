using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private float speed, conveyorSpeed; // Speed settings for the conveyor belt
    [SerializeField] private Vector3 direction; // Direction for the conveyor belt to move objects
    [SerializeField] private List<GameObject> onBelt; // List of GameObjects currently on the conveyor belt

    private Material material; // Material for the conveyor belt, not used in the current context

    [SerializeField] public TicketHolder ticketHolder; // Reference to the TicketHolder which manages tickets
    public Dictionary<int, List<string>> tickets; // Dictionary to hold ticket information

    public List<int> toRemove; // List of tickets to remove, not used in the current context

    private Vector3 MAX_FORCE; // Maximum force that can be applied to an object on the belt

    void Start()
    {
        MAX_FORCE = new Vector3(10.0f, 10.0f, 10.0f); // Initialize the maximum force
    }

    private void Update()
    {
        tickets = ticketHolder.GetComponent<TicketHolder>().tickets;
        if (onBelt.Count > 0)
        {
            CheckTickets();
        }
        else
        {
            foreach (var ticketKey in tickets.Keys)
            {
                ticketHolder.ticketGameObjects[ticketKey].GetComponent<TicketController>().SetMatchedIngredients(new List<string>());
            }
        }
    }

    void FixedUpdate()
    {
        foreach (GameObject obj in onBelt)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null && rb.GetAccumulatedForce().z <= MAX_FORCE.z)
            {
                rb.AddForce(speed * direction, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pizza")
        {
            onBelt.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
    }

    public void CheckTickets()
    {
        foreach (int ticketKey in tickets.Keys.ToList())
        {
            HashSet<string> cumulativeMatchedIngredients = new HashSet<string>();

            foreach (GameObject pizza in onBelt)
            {
                List<string> pizzaIngredients = pizza.GetComponent<PizzaController>().IngredientsList;

                // Check if the pizza could potentially match the ticket
                bool couldMatchTicket = pizzaIngredients.All(ingredient => tickets[ticketKey].Contains(ingredient));

                if (couldMatchTicket)
                {
                    // Add matched ingredients to the set
                    foreach (string ingredient in pizzaIngredients)
                    {
                        cumulativeMatchedIngredients.Add(ingredient);
                    }

                    // Check for a complete match
                    if (tickets[ticketKey].All(pizzaIngredients.Contains) && tickets[ticketKey].Count == pizzaIngredients.Count)
                    {
                        ProcessTicketAndPizza(ticketKey, pizza); // Process the complete match
                        return; // Exit the method to avoid further processing as the ticket and pizza have been handled
                    }
                }
            }

            // Update the ticket's matched ingredients only if there are any matches
            if (cumulativeMatchedIngredients.Count > 0)
            {
                ticketHolder.ticketGameObjects[ticketKey].GetComponent<TicketController>().SetMatchedIngredients(cumulativeMatchedIngredients.ToList());
            }
            else
            {
                ticketHolder.ticketGameObjects[ticketKey].GetComponent<TicketController>().SetMatchedIngredients(new List<string>());
            }
        }
    }

    void ProcessTicketAndPizza(int ticketKey, GameObject pizza)
    {
        ticketHolder.DeleteTicket(ticketKey);

        PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        // playerController.score += pizza.GetComponent<PizzaController>().pointsToAdd;
        playerController.score += 1;
        playerController.IncrementPizzaBox();

        onBelt.Remove(pizza);
        Destroy(pizza); 
    }
}