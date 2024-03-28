using System.Collections.Generic;
using UnityEngine;

public class VFXClean : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public GameObject smallGameObjectPrefab;
    public float spacing = 0.1f;

    private List<GameObject> toCleanObjects = new List<GameObject>();
    private List<GameObject> smallGameObjects = new List<GameObject>();

    void Start()
    {
        // Find all GameObjects with the "ToClean" tag
        GameObject[] toCleanObjectsArray = GameObject.FindGameObjectsWithTag("ToClean");
        toCleanObjects.AddRange(toCleanObjectsArray);

        Vector2 spriteSize = spriteRenderer.bounds.size;
        int columns = Mathf.CeilToInt(spriteSize.x / spacing);
        int rows = Mathf.CeilToInt(spriteSize.y / spacing);

        Vector3 startPos = spriteRenderer.bounds.min;

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Vector3 position = startPos + new Vector3(i * spacing, j * spacing, 0);

                if (IsSuitableForSpawn(position))
                {
                    GameObject smallObject = Instantiate(smallGameObjectPrefab, position, Quaternion.identity, transform);
                    smallGameObjects.Add(smallObject);
                }
            }
        }

        // Batch instantiate the small game objects
        InstantiateBatch();
    }

    private void Update()
    {
        // If there are no "ToClean" objects, log "Win" and return
        if (toCleanObjects.Count == 0)
        {
            Debug.Log("Win");
            return;
        }

    }
    bool IsSuitableForSpawn(Vector3 position)
    {
        // Raycast to check if there's a collider with the "Statue" tag nearby
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero, 0f);
        return hit.collider != null && hit.collider.CompareTag("Statue");
    }

    void InstantiateBatch()
    {
        // Disable all objects before enabling them together
        foreach (var go in smallGameObjects)
        {
            go.SetActive(false);
        }

        // Enable all objects together
        foreach (var go in smallGameObjects)
        {
            go.SetActive(true);
        }
    }
}
