using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketHolder : MonoBehaviour
{
    public List<string> possibleIngredients;
    public Dictionary<int, List<string>> tickets;

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
        if(numberOfTickets < 2)
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
    }

    public List<string> GetRandomCombo()
    {
        List<string> ans = new List<string>();

        int numToppings = Random.Range(1, maxToppings+1);
        //Get number of toppings for the ticket
        List<int> nums = new List<int>();
        while (nums.Count < numToppings)
        //Populate nums with unique indices corresponding to possible ingrdients
        //Populates nums to same size as numToppings
        {
            int pos = Random.Range(0, possibleIngredients.Count);
            if(!nums.Contains(pos))
            {
                nums.Add(pos);
            }
        }

        foreach( var x in nums)
        {
            Debug.Log( x.ToString());
        }

        // Debug.Log(nums);
        for(int i = 0; i < nums.Count; i++)
        {
            ans.Add(possibleIngredients[nums[i]]);
        }
        return ans;
    }

    public void DeleteTicket(int index)
    {
        
    }
}
