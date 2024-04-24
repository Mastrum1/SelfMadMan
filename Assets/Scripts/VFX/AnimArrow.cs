using System.Collections;
using UnityEngine;

public class AnimArrow : MonoBehaviour
{
    [SerializeField] private float lerpDistance = 50f; // Distance to lerp
    [SerializeField] private float lerpDuration = 0.2f; // Duration of lerp
    [SerializeField] private float bounceDuration = 0.5f; // Duration of bounce

    private Vector3 originalPosition;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        StartCoroutine(MoveCoroutine());
    }

    // Coroutine to handle the movement
    private IEnumerator MoveCoroutine()
    {
        while (true)
        {
            yield return StartCoroutine(LerpMove(originalPosition + transform.right * lerpDistance, lerpDuration));
            yield return StartCoroutine(LerpMove(originalPosition, bounceDuration));
        }
    }

    // Coroutine for lerping movement
    private IEnumerator LerpMove(Vector3 targetPosition, float duration)
    {
        float timeElapsed = 0f;
        Vector3 startPosition = transform.position;


        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition; // Ensure final position is exact
    }
}
