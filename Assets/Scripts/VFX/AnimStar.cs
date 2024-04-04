using System.Collections;
using UnityEngine;

public class AnimStar : MonoBehaviour
{
    public float scaleDuration = 1f;
    public float targetScale = 2f;
    public float spinSpeed = 1000f;
    public GameObject particleEffectPrefab;
    public GameObject nextStarPrefab; // Prefab for the next star
    public float nextStarScaleMultiplier = 3f; // Multiplier for next star scale

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

        // Spawn next star
        SpawnNextStar();
    }

    void SpawnParticleEffect()
    {
        Instantiate(particleEffectPrefab, transform.position, Quaternion.identity);
    }

    void SpawnNextStar()
    {
        GameObject nextStar = Instantiate(nextStarPrefab, transform.position, Quaternion.identity);
        StartCoroutine(GrowAndFade(nextStar));
    }

    IEnumerator GrowAndFade(GameObject star)
    {
        float duration = scaleDuration / 2f; // Make it grow 5 times faster
        float timeElapsed = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = originalScale * targetScale * nextStarScaleMultiplier; // Grow three times bigger

        while (timeElapsed < duration)
        {
            star.transform.localScale = Vector3.Lerp(startScale, endScale, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure scale is set to exact target at the end
        star.transform.localScale = endScale;

        // Fade away
        float fadeDuration = 1f;
        timeElapsed = 0f;
        Material starMaterial = star.GetComponent<Renderer>().material;
        Color startColor = starMaterial.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (timeElapsed < fadeDuration)
        {
            starMaterial.color = Color.Lerp(startColor, endColor, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Destroy the star when faded
        Destroy(star);
    }
}
