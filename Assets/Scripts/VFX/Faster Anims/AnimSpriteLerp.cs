using System.Collections;
using UnityEngine;

public class AnimSpriteLerp : MonoBehaviour
{
    [SerializeField] private float lerpDuration = 1f;
    [SerializeField] private float distanceToMove = 1f;
    [SerializeField] private float waitingTime = 1f;

    private void Start()
    {
        StartCoroutine(StartLerpAfterDelay());
    }

    private IEnumerator StartLerpAfterDelay()
    {
        yield return new WaitForSeconds(waitingTime); // Delay for 1 second before starting the lerp
        StartCoroutine(LerpRight());
    }

    private IEnumerator LerpRight()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + new Vector3(distanceToMove, 0f, 0f);
        Vector3 midPos = startPos + new Vector3(distanceToMove / 2f, 0f, 0f); // Midpoint position
        float elapsedTime = 0f;
        bool slowingDown = false;

        while (elapsedTime < lerpDuration)
        {
            if (!slowingDown && elapsedTime >= lerpDuration / 2f) // Check if we reached the midpoint
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

            float t = Mathf.Clamp01(elapsedTime / lerpDuration);
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        // Ensure we reach the exact position at the end of lerp
        transform.position = endPos;
    }
}
