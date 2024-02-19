using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject ingredient;

    [SerializeField]
    private int maxIngredients = 5;

    [SerializeField]
    private string ingredientTag;

    private List<GameObject> currentIngredients = new List<GameObject>();

    private void Update()
    {
        // Clean up any null references in the list (for objects that have been deleted)
        CleanupIngredientsList();

        // Check if we need to spawn new ingredients
        if (currentIngredients.Count < maxIngredients)
        {
            SpawnIngredient();
        }

        // Logging the count for debugging purposes
        // if (ingredientTag == "Pepperoni")
        // {
        //     Debug.Log(currentIngredients.Count);
        // }
    }

    private void SpawnIngredient()
    {
        Vector3 spawnPosition = GetRandomPositionInSpawner();
        GameObject newIngredient = Instantiate(ingredient, spawnPosition, Quaternion.identity, transform);
        newIngredient.layer = LayerMask.NameToLayer("Interactable");
        currentIngredients.Add(newIngredient);
    }

    private Vector3 GetRandomPositionInSpawner()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        Vector3 spawnPoint = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
        );
        return spawnPoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(ingredientTag))
        {
            currentIngredients.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(ingredientTag))
        {
            currentIngredients.Remove(other.gameObject);
        }
    }

    // No need for OnTriggerExit as we handle deletion cleanup in CleanupIngredientsList()

    // Cleanup null references in the currentIngredients list
    private void CleanupIngredientsList()
    {
        for (int i = currentIngredients.Count - 1; i >= 0; i--)
        {
            if (currentIngredients[i] == null)
            {
                currentIngredients.RemoveAt(i);
            }
        }
    }
}