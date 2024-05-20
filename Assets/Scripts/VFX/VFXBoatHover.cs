using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXBo : MonoBehaviour
{
    public float slideAmount = 0.1f; // Adjust the amount of sliding
    public float slideSpeed = 2f; // Adjust the speed of sliding

    private Vector3 startPosition;

    void Start()
    {
        // Save the initial position of the object
        startPosition = transform.position;
        StartCoroutine(Hover());
    }

    private IEnumerator Hover()
    {
        while (true)
        {
        float horizontalMovement = Mathf.Sin(Time.time * slideSpeed) * slideAmount;
        float verticalMovement = Mathf.Cos(Time.time * slideSpeed) * slideAmount;

        // Update the position of the object
        transform.position = startPosition + new Vector3(horizontalMovement, verticalMovement, 0f);

        yield return new WaitForSeconds(0.01f);
        }
    }
}
