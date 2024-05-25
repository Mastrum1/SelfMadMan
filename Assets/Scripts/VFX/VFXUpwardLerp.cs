using UnityEngine;

public class VFXUpwardLerp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f; // Speed of the upward movement

    [SerializeField]
    private float _distance = 1.0f; // Distance to move upward

    private Vector3 _targetPosition;
    private Vector3 _initialPosition;
    private float _startTime;
    private bool _reachedTarget;

    private void Start()
    {
        _initialPosition = transform.position;
        _targetPosition = _initialPosition + Vector3.up * _distance;
        _startTime = Time.time; // Record the start time
    }

    private void Update()
    {
        if (!_reachedTarget)
        {
            // Calculate the percentage of completion based on current position and target position
            float journeyLength = Vector3.Distance(_initialPosition, _targetPosition);
            float distCovered = (Time.time - _startTime) * _speed;
            float fracJourney = distCovered / journeyLength;

            // Move the object towards the target position using Lerp
            transform.position = Vector3.Lerp(_initialPosition, _targetPosition, fracJourney);

            // If the object has reached or surpassed the target position, stop moving
            if (transform.position.y >= _targetPosition.y)
            {
                transform.position = _targetPosition;
                _reachedTarget = true;
                _startTime = Time.time + 0.5f; // Delay the return to original position
            }
        }
        else
        {
            // After 0.5 seconds delay, teleport back to the original position
            if (Time.time >= _startTime)
            {
               mySceneManager.instance.LoadWinScreen();
               mySceneManager.instance.LoadHomeScreen();
            }
        }
    }
}
