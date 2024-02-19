using UnityEngine;
using System.Collections;

public class PizzaBoxSpawnScript : MonoBehaviour
{
    public GameObject pizzaBoxPrefab; // Assign this in the inspector with your pizza prefab

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SpawnPizzaBox()
    {
        Instantiate(pizzaBoxPrefab, transform.position, Quaternion.identity); // Spawn the pizza at the position of the GameObject this script is attached to
        Vector3 temp = new Vector3(0.0f, 0.3f, 0);
        transform.position += temp;
    }
}