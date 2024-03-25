using System.Collections;
using UnityEngine;

public class VFXCircleRotate : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float scaleSpeed = 100f;
    public float maxScale = 2f;
    public float minScale = 0.5f;

    private IEnumerator Start()
    {
        // Start the scaling coroutine
        yield return StartCoroutine(ScaleCoroutine());
    }

    private IEnumerator ScaleCoroutine()
    {
        while (true)
        {
            // Scale up
            while (transform.localScale.x < maxScale)
            {
                transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime * 10;
                yield return null;
            }

            // Scale down
            while (transform.localScale.x > minScale)
            {
                transform.localScale -= Vector3.one * scaleSpeed * Time.deltaTime * 10;
                yield return null;
            }
        }
    }

    private void Update()
    {
        // Rotate the object in 2D
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}