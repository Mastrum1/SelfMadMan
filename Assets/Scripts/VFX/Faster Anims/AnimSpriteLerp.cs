using System.Collections;
using UnityEngine;

public class AnimSpriteLerp : MonoBehaviour
{
    [SerializeField] private float lerpDurationLeft = 1f;
    [SerializeField] private float lerpDurationRight = 1f;
    [SerializeField] private float distanceToMoveLeft = 1f;
    [SerializeField] private float distanceToMoveRight = 1f;
    [SerializeField] private float waitingTime = 1f;

    private void Start()
    {
        StartCoroutine(StartLerpAfterDelay());
    }

    private IEnumerator StartLerpAfterDelay()
    {
        yield return new WaitForSeconds(waitingTime); // Delay for 1 second before starting the lerp
        StartCoroutine(LerpLeft());
    }

    private IEnumerator LerpLeft()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos - new Vector3(distanceToMoveLeft, 0f, 0f);
        float elapsedTime = 0f;

        while (elapsedTime < lerpDurationLeft)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / lerpDurationLeft);
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        // Ensure we reach the exact position at the end of lerp
        transform.position = endPos;

        // Start lerping to the right after lerping to the left
        StartCoroutine(LerpRight());
    }

    private IEnumerator LerpRight()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + new Vector3(distanceToMoveRight, 0f, 0f);
        Vector3 midPos = startPos + new Vector3(distanceToMoveRight / 2f, 0f, 0f); // Midpoint position
        float elapsedTime = 0f;
        bool slowingDown = false;

        while (elapsedTime < lerpDurationRight)
        {
            if (!slowingDown && elapsedTime >= lerpDurationRight / 2f) // Check if we reached the midpoint
            {
                slowingDown = true;
                yield return new WaitForSeconds(1f); // Slow down for 1 second
            }

            if (slowingDown)
            {
                elapsedTime += Time.deltaTime * 0.5f; // Slow down speed
            }
            else
            {
                elapsedTime += Time.deltaTime;
            }

            float t = Mathf.Clamp01(elapsedTime / lerpDurationRight);
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        // Ensure we reach the exact position at the end of lerp
        transform.position = endPos;
    }
}
