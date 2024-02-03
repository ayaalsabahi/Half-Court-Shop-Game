using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PepperoniSpawner : MonoBehaviour
{
    public GameObject pepperoni;
    public int maxPepperonis = 5;

    private List<GameObject> currentPepperonis = new List<GameObject>();

    private void Update()
    {
        // Check if we need to spawn new pepperonis
        if (currentPepperonis.Count < maxPepperonis)
        {
            SpawnPepperoni();
        }
    }

    private void SpawnPepperoni()
    {
        // Instantiate a new pepperoni inside the bounds of the collider
        Vector3 spawnPosition = GetRandomPositionInSpawner();
        GameObject newPepperoni = Instantiate(pepperoni, spawnPosition, Quaternion.identity, transform);
        newPepperoni.layer = LayerMask.NameToLayer("Interactable");
        newPepperoni.tag = "Pepperoni";
        currentPepperonis.Add(newPepperoni);
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
        if (other.gameObject.CompareTag("Pepperoni"))
        {
            currentPepperonis.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Pepperoni"))
        {
            currentPepperonis.Remove(other.gameObject);
        }
    }
}
