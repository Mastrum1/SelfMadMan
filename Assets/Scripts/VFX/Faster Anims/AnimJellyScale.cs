using System.Collections;
using UnityEngine;

public class AnimJellyScale : MonoBehaviour
{
    [SerializeField]
    private float minXScale = 0.5f; // Minimum X scale value
    [SerializeField]
    private float maxXScale = 1.5f; // Maximum X scale value
    [SerializeField]
    private float minYScale = 1f; // Minimum Y scale value
    [SerializeField]
    private float maxYScale = 2f; // Maximum Y scale value
    [SerializeField]
    private float duration = 1f; // Duration of the scale animation

    private void Start()
    {
        StartCoroutine(ScaleCoroutine());
    }

    private IEnumerator ScaleCoroutine()
    {
        while (true)
        {
            yield return ScaleObject(new Vector2(minXScale, minYScale), new Vector2(maxXScale, maxYScale));
            yield return ScaleObject(new Vector2(maxXScale, maxYScale), new Vector2(minXScale, minYScale));
        }
    }

    private IEnumerator ScaleObject(Vector2 startScale, Vector2 endScale)
    {
        float timeElapsed = 0f;
        Vector3 initialScale = transform.localScale;
        Vector3 targetScale = new Vector3(endScale.x, endScale.y, initialScale.z);

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            Vector2 lerpedScale = Vector2.Lerp(startScale, endScale, t);
            transform.localScale = new Vector3(lerpedScale.x, lerpedScale.y, initialScale.z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure final scale is set
        transform.localScale = targetScale;
    }
}
