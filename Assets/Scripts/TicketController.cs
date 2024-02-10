using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TicketController : MonoBehaviour
{
    public List<string> ingredients;
    
    [SerializeField]
    public TMP_Text ingredientsText;

    int pointsToEarn;

    // Start is called before the first frame update
    void Start()
    {
        ingredientsText.text = OragnizeIngredients();
    }

    // Update is called once per frame
    void Update()
    {
        pointsToEarn = ingredients.Count;
    }

    public string OragnizeIngredients()
    {
        string ans = "";
        for(int i = 0; i < ingredients.Count; i++)
        {
            ans += ingredients[i] + "\n";
        }
        return ans;
    }
}
