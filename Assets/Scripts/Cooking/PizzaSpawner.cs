using UnityEngine;
using System.Collections;

public class PizzaSpawner : MonoBehaviour
{
    public GameObject pizzaPrefab; // Assign this in the inspector with your pizza prefab
    public float spawnInterval = 1.0f; // Time between each spawn

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnPizzaRoutine());
    }

    private IEnumerator SpawnPizzaRoutine()
    {
        while (true) // Infinite loop to keep spawning pizzas
        {
            yield return new WaitForSeconds(spawnInterval); // Wait for specified interval
            SpawnPizza();
        }
    }

    private void SpawnPizza()
    {
        Instantiate(pizzaPrefab, transform.position, Quaternion.identity); // Spawn the pizza at the position of the GameObject this script is attached to
    }
}