using System.Collections;
using UnityEngine;

public class AnimStar : MonoBehaviour
{
    public float scaleDuration = 1f;
    public float targetScale = 2f;
    public float spinSpeed = 1000f;
    public GameObject particleEffectPrefab;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
        StartCoroutine(ScaleObject());
    }

    IEnumerator ScaleObject()
    {
        float timeElapsed = 0f;
        Vector3 startScale = originalScale;
        Vector3 endScale = originalScale * targetScale;

        while (timeElapsed < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, timeElapsed / scaleDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure scale is set to exact target at the end
        transform.localScale = endScale;

        // Spin really fast
        float spinTime = 0.5f;
        float spinElapsed = 0f;
        while (spinElapsed < spinTime)
        {
            transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
            spinElapsed += Time.deltaTime;
            yield return null;
        }

        // Reset rotation
        transform.rotation = Quaternion.identity;

        // Scale back to original
        timeElapsed = 0f;
        startScale = endScale;
        endScale = originalScale;

        while (timeElapsed < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, timeElapsed / scaleDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure scale is set to exact target at the end
        transform.localScale = endScale;

        // Spawn particle effect
        SpawnParticleEffect();
    }

    void SpawnParticleEffect()
    {
        Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
    }
}
