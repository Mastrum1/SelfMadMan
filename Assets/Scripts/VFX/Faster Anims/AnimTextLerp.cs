using UnityEngine;
using System.Collections;

public class AnimTextLerp : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 100.0f)]
    private float upSpeed = 1.0f; // Speed of upward movement

    [SerializeField]
    [Range(0.1f, 100.0f)]
    private float downSpeed = 1.0f; // Speed of downward movement

    [SerializeField]
    [Range(0.1f, 100.0f)]
    private float upDistance = 1.0f; // Distance to move upward

    [SerializeField]
    [Range(0.1f, 100.0f)]
    private float downDistance = 1.0f; // Distance to move downward

    [SerializeField]
    [Range(0.1f, 100.0f)]
    private float waitForTime = 1.0f; // Distance to move downward


    void Start()
    {
        StartCoroutine(MoveUpAndDown());
    }

    IEnumerator MoveUpAndDown()
    {
        yield return new WaitForSeconds(0.2f);
        yield return StartCoroutine(MoveObject(transform.position, transform.position + Vector3.up * upDistance, upSpeed));
        yield return new WaitForSeconds(waitForTime); // Delay for 1 second
        yield return StartCoroutine(MoveObject(transform.position, transform.position + Vector3.down * downDistance, downSpeed));

        // Once the up and down movement is finished, go up fast indefinitely
        while (true)
        {
            transform.Translate(Vector3.up * upSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator MoveObject(Vector3 startPosition, Vector3 targetPosition, float speed)
    {
        float startTime = Time.time;
        float journeyLength = Vector3.Distance(startPosition, targetPosition);

        while (true)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(startPosition, targetPosition, fracJourney);

            if (fracJourney >= 1.0f)
            {
                break;
            }

            yield return null;
        }
    }
}
