using System.Collections;
using UnityEngine;

public class VFXScaleUp : MonoBehaviour
{
    [SerializeField] private float scaleUpDuration = 0.5f;
    [SerializeField] private float scaleDownDuration = 0.5f;
    [SerializeField] private float scaleUpFactor = 1.3f;
    private bool isScaling = false;
    private Vector3 originalScale;

    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (isScaling)
        {
            // Scale the object smoothly
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * 2f);
        }
    }

    // Function to call when the object is clicked
    public void OnObjectClicked()
    {
        // Check if not already scaling
        if (!isScaling)
        {
            StartCoroutine(ScaleObject());
        }
    }

    // Coroutine to scale the object
    private IEnumerator ScaleObject()
    {
        isScaling = true;

        // Scale up to 1.3
        float timer = 0f;
        Vector3 targetScale = originalScale * scaleUpFactor;

        while (timer < scaleUpDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, timer / scaleUpDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        // Scale down smoothly
        timer = 0f;
        while (timer < scaleDownDuration)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, timer / scaleDownDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale; // Ensure the scale is exactly the original scale at the end
        isScaling = false;
    }
}
