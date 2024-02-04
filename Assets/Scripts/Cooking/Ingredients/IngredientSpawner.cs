using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject ingredient;

    [SerializeField]
    public int maxIngredients = 5;

    [SerializeField]
    public string ingredientTag;

    private List<GameObject> currentIngredients = new List<GameObject>();

    private void Update()
    {
        // Check if we need to spawn new pepperonis
        if (currentIngredients.Count < maxIngredients)
        {
            SpawnPepperoni();
        }
    }

    private void SpawnPepperoni()
    {
        // Instantiate a new pepperoni inside the bounds of the collider
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
}
