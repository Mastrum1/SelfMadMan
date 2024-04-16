using System.Collections;
using UnityEngine;

public class VignetteLerper : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private float duration = 1f;
    [SerializeField] private float startValue = 1.7f; // 1.7f to 0f for closing vignette / 0f to 1.7f for opening vignette
    [SerializeField] private float endValue = 0f;

    private void OnEnable()
    {
        StartCoroutine(LerpVignetteSize());
    }

    private IEnumerator LerpVignetteSize()
    {
        float elapsedTime = 0f;

        // Lerping to end value
        while (elapsedTime < duration)
        {
            float newValue = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            material.SetFloat("_VignetteSize", newValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the end value is set
        material.SetFloat("_VignetteSize", endValue);

        // Reset elapsed time for lerping back
        elapsedTime = 0f;

        // Lerping back to start value
        while (elapsedTime < duration)
        {
            float newValue = Mathf.Lerp(endValue, startValue, elapsedTime / duration);
            material.SetFloat("_VignetteSize", newValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the start value is set
        material.SetFloat("_VignetteSize", startValue);
    }
}
