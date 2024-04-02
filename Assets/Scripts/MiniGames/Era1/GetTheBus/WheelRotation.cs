using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    // Update is called once per frame
    [SerializeField] float _mSpeed = 25;
    void Update()
    {
        transform.Rotate(Vector3.back * 45 * Time.deltaTime * _mSpeed, Space.Self);
    }
}
