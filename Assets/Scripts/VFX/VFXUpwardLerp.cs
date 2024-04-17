using UnityEngine;

public class VFXUpwardLerp : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f; // Speed of the upward movement

    [SerializeField]
    private float distance = 1.0f; // Distance to move upward

    private Vector3 targetPosition;
    private Vector3 initialPosition;
    private float startTime;

    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + Vector3.up * distance;
        startTime = Time.time; // Record the start time
    }

    private void Update()
    {
        // Calculate the percentage of completion based on current position and target position
        float journeyLength = Vector3.Distance(initialPosition, targetPosition);
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;

        // Move the object towards the target position using Lerp
        transform.position = Vector3.Lerp(initialPosition, targetPosition, fracJourney);

        // If the object has reached or surpassed the target position, stop moving
        if (transform.position.y >= targetPosition.y)
        {
            transform.position = targetPosition;
            enabled = false; // Disable this script to stop further updates
        }
    }
}
