using System.Collections;
using UnityEngine;

public class VFXTilt : MonoBehaviour
{
    public float tiltAngle = 15f; // Maximum tilt angle
    public float tiltSpeed = 1000f; // Tilt speed in degrees per second
    public float returnSpeed = 500f; // Return speed in degrees per second
    public float interval = 3f; // Time interval between tilts

    private bool isTilting = false; // Flag to check if currently tilting

    void Start()
    {
        StartCoroutine(TiltCoroutine());
    }

    IEnumerator TiltCoroutine()
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
