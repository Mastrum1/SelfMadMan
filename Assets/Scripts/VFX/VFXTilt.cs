using System.Collections;
using UnityEngine;

public class VFXTilt : MonoBehaviour
{
    [SerializeField]
    private float tiltAngle = 15f; // Maximum tilt angle
    [SerializeField]
    private float tiltSpeed = 1000f; // Tilt speed in degrees per second
    [SerializeField]
    private float returnSpeed = 500f; // Return speed in degrees per second
    [SerializeField]
    private float interval = 3f; // Time interval between tilts

    private bool isTilting = false; // Flag to check if currently tilting

    private void Start()
    {
        StartCoroutine(TiltCoroutine());
    }

    private IEnumerator TiltCoroutine()
    {
        while (true)
        {
            if (!isTilting)
            {
                isTilting = true;

                // Tilt right
                float elapsedTime = 0f;
                float startRotation = transform.rotation.eulerAngles.z;
                float targetRotationRight = startRotation + tiltAngle;
                while (elapsedTime < interval / 2)
                {
                    float newRotation = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.z, targetRotationRight, tiltSpeed * Time.deltaTime * 5);
                    transform.rotation = Quaternion.Euler(0, 0, newRotation);
                    elapsedTime += Time.deltaTime * 10;
                    yield return null;
                }

                // Tilt left
                elapsedTime = 0f;
                float targetRotationLeft = startRotation - tiltAngle;
                while (elapsedTime < interval / 2)
                {
                    float newRotation = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.z, targetRotationLeft, tiltSpeed * Time.deltaTime * 5);
                    transform.rotation = Quaternion.Euler(0, 0, newRotation);
                    elapsedTime += Time.deltaTime * 10;
                    yield return null;
                }

                // Return to original rotation
                while (transform.rotation.eulerAngles.z != startRotation)
                {
                    float newRotation = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.z, startRotation, returnSpeed * Time.deltaTime * 5);
                    transform.rotation = Quaternion.Euler(0, 0, newRotation);
                    yield return null;
                }

                isTilting = false;
            }

            yield return new WaitForSeconds(interval);
        }
    }
}
