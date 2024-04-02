using System.Collections;
using UnityEngine;

public class VignetteLerper : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private float duration = 1f; 
    [SerializeField] private float startValue = 1.7f; // 1.7f to 0f for closing vignette / 0f to 1.7f for opening vignette
    [SerializeField] private float endValue = 0f;

    private void Start()
    {
        StartCoroutine(LerpVignetteSize());
    }

    private IEnumerator LerpVignetteSize()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float newValue = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            material.SetFloat("_VignetteSize", newValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        material.SetFloat("_VignetteSize", endValue);
    }
}
