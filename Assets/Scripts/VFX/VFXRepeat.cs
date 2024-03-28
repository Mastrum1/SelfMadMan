using System.Collections;
using UnityEngine;

public class VFXRepeat : MonoBehaviour
{
    public GameObject prefab;
    private GameObject instantiatedObject; 

    void Start()
    {
        StartCoroutine(RepeatInstantiate());
    }

    IEnumerator RepeatInstantiate()
    {
        while (true)
        {
            // Instantiate the object
            instantiatedObject = Instantiate(prefab, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(2f);

            // Destroy the old object
            Destroy(instantiatedObject);

            yield return new WaitForSeconds(1f);
        }
    }
}