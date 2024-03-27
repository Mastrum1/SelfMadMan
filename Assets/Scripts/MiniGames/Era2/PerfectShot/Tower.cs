using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Tower : MonoBehaviour
{
    private bool _rotating = true;
    public bool Rotating { get => _rotating; set => _rotating = value; }

    private float _rotationSpeed = 25f;
    private float _minRotation = -15f;
    private float _maxRotation = 25f;
    private float _targetRotation;

    [NonSerialized] public float CurrentRotation;
    [SerializeField] private RectTransform _rectTransform;

    void Update()
    {
        if (Rotating)
        {
            float currentRotation = Mathf.MoveTowardsAngle(_rectTransform.localEulerAngles.z, _targetRotation, _rotationSpeed * Time.deltaTime);
            _rectTransform.localEulerAngles = new Vector3(_rectTransform.localEulerAngles.x, _rectTransform.localEulerAngles.y, currentRotation);

            CurrentRotation = NormalizeAngle(_rectTransform.localEulerAngles.z);
            if (Mathf.Approximately(currentRotation, _targetRotation))
            {
                _targetRotation = (_targetRotation == _minRotation) ? _maxRotation : _minRotation;
            }
        }
    }

    private float NormalizeAngle(float angle)
    {
        // Ensure angle stays within -180 to 180 degrees
        angle %= 360f;
        if (angle > 180f)
            angle -= 360f;
        else if (angle < -180f)
            angle += 360f;
        return angle;
    }
}
