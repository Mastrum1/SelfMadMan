using System.Collections.Generic;
using UnityEngine;

public class VFXClean : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Reference to the sprite renderer component
    public GameObject smallGameObjectPrefab; // Prefab for small game objects
    public float spacing = 0.1f; // Spacing between small game objects

    void Start()
    {
        Vector2 spriteSize = spriteRenderer.bounds.size;
        int columns = Mathf.CeilToInt(spriteSize.x / spacing);
        int rows = Mathf.CeilToInt(spriteSize.y / spacing);

        Vector3 startPos = spriteRenderer.bounds.min;

        // Batch instantiation list
        List<GameObject> smallGameObjects = new List<GameObject>();

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Vector3 position = startPos + new Vector3(i * spacing, j * spacing, 0);

                // Check if the position is suitable for spawning
                if (IsSuitableForSpawn(position))
                {
                    // Instantiate the small game object and add it to the batch list
                    smallGameObjects.Add(Instantiate(smallGameObjectPrefab, position, Quaternion.identity, transform));
                }
            }
        }

        // Batch instantiate the small game objects
        InstantiateBatch(smallGameObjects);
    }

    bool IsSuitableForSpawn(Vector3 position)
    {
        // Raycast to check if there's a collider with the "Statue" tag nearby
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero, 0f);
        return hit.collider != null && hit.collider.CompareTag("Statue");
    }

    void InstantiateBatch(List<GameObject> gameObjects)
    {
        // Disable all objects before enabling them together
        foreach (var go in gameObjects)
        {
            go.SetActive(false);
        }

        // Enable all objects together
        foreach (var go in gameObjects)
        {
            go.SetActive(true);
        }
    }
}
