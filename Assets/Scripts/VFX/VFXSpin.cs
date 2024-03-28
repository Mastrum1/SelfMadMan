using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXSpin : MonoBehaviour
{
    public float spinSpeed = 1f; 
    public float radius = 2f; 

    private Vector2 pivotPoint; 

    void Start()
    {
        pivotPoint = transform.position;
    }

    void Update()
    {
        float angle = Time.time * spinSpeed;

        float x = pivotPoint.x + Mathf.Cos(angle) * radius;
        float y = pivotPoint.y + Mathf.Sin(angle) * radius;

        transform.position = new Vector3(x, y, transform.position.z);
        transform.rotation = Quaternion.Euler(0, 0, -angle * Mathf.Rad2Deg);
    }
}
