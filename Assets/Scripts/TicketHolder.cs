using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TicketHolder : MonoBehaviour
{
    public List<string> possibleIngredients;
    public Dictionary<int, List<string>> tickets = new Dictionary<int, List<string>>();
    public Dictionary<int, GameObject> ticketGameObjects = new Dictionary<int, GameObject>();

    [SerializeField]
    public GameObject ticketPrefab;

    public int maxToppings = 4;

    public int numberOfTickets;
    public int curTicketNum;

    // Start is called before the first frame update
    void Start()
    {
        possibleIngredients.Add("Pepperoni");
        possibleIngredients.Add("Mushroom");
        possibleIngredients.Add("Olive");
        possibleIngredients.Add("Pineapple");
        numberOfTickets = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (tickets.Count < 2)
        {
            CreateTicket();
        }
    }

    public void CreateTicket()
    {
        curTicketNum = numberOfTickets;
        numberOfTickets += 1;
        GameObject newTicket = Instantiate(ticketPrefab, this.transform);
        newTicket.GetComponent<TicketController>().ingredients = GetRandomCombo();
        tickets.Add(curTicketNum, newTicket.GetComponent<TicketController>().ingredients);
        ticketGameObjects.Add(curTicketNum, newTicket); // Track the GameObject for the new ticket
    }

    public List<string> GetRandomCombo()
    {
        List<string> ans = new List<string>();
        if (curTicketNum < 2)
        {
            ans.Add("Pepperoni");
            return ans;
        }
        else
        {
            int numToppings = Random.Range(1, maxToppings + 1);
            List<int> nums = new List<int>();
            while (nums.Count < numToppings)
            {
                int pos = Random.Range(0, possibleIngredients.Count);
                if (!nums.Contains(pos))
                {
                    nums.Add(pos);
                }
            }

            for (int i = 0; i < nums.Count; i++)
            {
                ans.Add(possibleIngredients[nums[i]]);
            }
            return ans;
        }
    }

    public void DeleteTicket(int index)
    {
        Debug.Log($"Before deletion, tickets: {DictionaryToString(tickets)}");

        // Attempt to delete the ticket and its GameObject
        if (tickets.ContainsKey(index))
        {
            tickets.Remove(index);

            if (ticketGameObjects.ContainsKey(index))
            {
                Destroy(ticketGameObjects[index]); // Destroy the GameObject
                ticketGameObjects.Remove(index); // Remove the reference from the dictionary
            }
        }

        Debug.Log($"After deletion, tickets: {DictionaryToString(tickets)}");
    }

    private string DictionaryToString(Dictionary<int, List<string>> dictionary)
    {
        return "{" + string.Join(", ", dictionary.Select(kv => kv.Key + "=[" + string.Join(", ", kv.Value) + "]")) + "}";
    }
}