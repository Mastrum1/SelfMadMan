using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXSpin : MonoBehaviour
{
    public float spinSpeed = 1f; // Speed of rotation in degrees per second
    public float radius = 2f; // Radius of the circular path

    private Vector2 pivotPoint; // Pivot point for rotation

    void Start()
    {
        // Set pivot point to current position
        pivotPoint = transform.position;
    }

    void Update()
    {
        // Calculate the angle based on time and spinSpeed
        float angle = Time.time * spinSpeed;

        // Calculate the new position using trigonometry
        float x = pivotPoint.x + Mathf.Cos(angle) * radius;
        float y = pivotPoint.y + Mathf.Sin(angle) * radius;

        // Update the object's position
        transform.position = new Vector3(x, y, transform.position.z);

        // Rotate the object to make it face the pivot point
        transform.rotation = Quaternion.Euler(0, 0, -angle * Mathf.Rad2Deg);
    }
}
