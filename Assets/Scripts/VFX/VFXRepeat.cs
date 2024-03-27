using System.Collections;
using UnityEngine;

public class VFXRepeat : MonoBehaviour
{
    public GameObject prefab; // Reference to the prefab to instantiate
    private GameObject instantiatedObject; // Reference to the instantiated object

    void Start()
    {
        // Start the repeating coroutine
        StartCoroutine(RepeatInstantiate());
    }

    IEnumerator RepeatInstantiate()
    {
        while (true)
        {
            // Instantiate the object
            instantiatedObject = Instantiate(prefab, transform.position, Quaternion.identity);

            // Wait for 2 seconds
            yield return new WaitForSeconds(2f);

            // Destroy the old object
            Destroy(instantiatedObject);

            // Wait for 1 second
            yield return new WaitForSeconds(1f);
        }
    }
}