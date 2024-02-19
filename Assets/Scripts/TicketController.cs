using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TicketController : MonoBehaviour
{
    public List<string> ingredients;
    [SerializeField]
    public TMP_Text ingredientsText;
    public int pointsToEarn;

    // List to track matched ingredients for color change
    private List<string> matchedIngredients = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        UpdateIngredientsDisplay();  // Updated to call the new method
    }

    // Update is called once per frame
    void Update()
    {
        pointsToEarn = ingredients.Count;
    }

    // Updated method to organize ingredients and apply color changes to matched ones
    public void UpdateIngredientsDisplay()
    {
        string displayText = "";
        foreach (var ingredient in ingredients)
        {
            if (matchedIngredients.Contains(ingredient))
            {
                displayText += "<color=green>" + ingredient + "</color>\n";  // Apply green color to matched ingredients
            }
            else
            {
                displayText += ingredient + "\n";  // Keep original color for unmatched ingredients
            }
        }
        ingredientsText.text = displayText;
    }

    // Method to update matched ingredients list and refresh the display
    public void SetMatchedIngredients(List<string> matches)
    {
        matchedIngredients = matches;
        UpdateIngredientsDisplay();  // Refresh the ingredient display with updated matches
    }
}