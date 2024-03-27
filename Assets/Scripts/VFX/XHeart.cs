using UnityEngine;
using System.Collections;

public class XHeart : MonoBehaviour
{
    public AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 0, 1, 1.3f);
    public float animationDuration = 0.33f; // Total duration for the animation, divided by 3
    public float scaleBackDuration = 0.167f; // Duration for scaling back to original size, divided by 3

    private Vector3 originalHeartScale;
    private bool isAnimating = false;

    void Start()
    {
        originalHeartScale = transform.localScale;
        transform.localScale = Vector3.zero; // Start with scale zero
        StartAnimation();
    }

    void StartAnimation()
    {
        if (!isAnimating)
        {
            isAnimating = true;
            StartCoroutine(HeartAnimation());
        }
    }

    IEnumerator HeartAnimation()
    {
        float timeElapsed = 0f;

        while (timeElapsed < animationDuration)
        {
            float t = timeElapsed / animationDuration;
            float scaleValue = scaleCurve.Evaluate(t);

            transform.localScale = originalHeartScale * scaleValue; // Apply scale based on curve

            yield return null;
            timeElapsed += Time.deltaTime * 3f; // Speed up by 3
        }

        // Ensure final scale is 1.3
        transform.localScale = originalHeartScale * scaleCurve.Evaluate(1);

        // Scale back to original size gradually
        float scaleBackStartTime = Time.time;
        while (Time.time < scaleBackStartTime + scaleBackDuration)
        {
            float t = (Time.time - scaleBackStartTime) / scaleBackDuration;

            // Lerp from maximum scale to original scale
            transform.localScale = Vector3.Lerp(originalHeartScale * scaleCurve.Evaluate(1), originalHeartScale, t);

            yield return null;
        }

        // Ensure final scale is original scale (1)
        transform.localScale = originalHeartScale;
    }
}
