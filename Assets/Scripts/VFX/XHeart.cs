using UnityEngine;
using System.Collections;

public class XHeart : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 0, 1, 1.3f);
    [SerializeField]
    private float animationDuration = 0.33f; 
    [SerializeField]
    private float scaleBackDuration = 0.167f; 

    private Vector3 originalHeartScale;
    private bool isAnimating = false;

    private void Start()
    {
        originalHeartScale = transform.localScale;
        transform.localScale = Vector3.zero; // Start with scale zero
        StartAnimation();
    }

    private void StartAnimation()
    {
        if (!isAnimating)
        {
            isAnimating = true;
            StartCoroutine(HeartAnimation());
        }
    }

    private IEnumerator HeartAnimation()
    {
        float timeElapsed = 0f;

        while (timeElapsed < animationDuration)
        {
            float t = timeElapsed / animationDuration;
            float scaleValue = scaleCurve.Evaluate(t);

            transform.localScale = originalHeartScale * scaleValue;

            yield return null;
            timeElapsed += Time.deltaTime * 3f; 
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
