using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Tower : MonoBehaviour
{

    private bool _rotating = true;
    public bool Rotating { get => _rotating; set => _rotating = value; }

    private float _rotationSpeed = 25;
    private float _ClockrotationSpeed;

    private float _minRotation = -15f;
    private float _maxRotation = 25f;
    private float _targetRotation;

    private float minClockRotation = -69.9f;
    private float maxClockRotation = 110f;
    private float _targetClockRotation;


    [NonSerialized] public float CurrentRotation;
    [NonSerialized] public float CurrentClockRotation;
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private RectTransform _Hand;

    private void Start()
    {
        if (GameManager.instance.FasterLevel < 4)
        {
            _rotationSpeed += GameManager.instance.FasterLevel * 20;
            _ClockrotationSpeed = _rotationSpeed * 4.5f;
        }
    }
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

            float clockRotation = Mathf.MoveTowardsAngle(_Hand.localEulerAngles.z, _targetClockRotation, _ClockrotationSpeed * Time.deltaTime);
            _Hand.localEulerAngles = new Vector3(_Hand.localEulerAngles.x, _Hand.localEulerAngles.y, clockRotation);
            Debug.Log("clock" + NormalizeAngle(clockRotation));
            Debug.Log(" target "  + NormalizeAngle(_targetClockRotation));
            if (Mathf.Approximately(clockRotation, _targetClockRotation))
            {
                _targetClockRotation = (_targetClockRotation == minClockRotation) ? maxClockRotation : minClockRotation;
            }

        }
    }

    private float NormalizeAngle(float angle)
    {
        // Ensure angle stays within -180 to 180 degrees
        angle %= 360f;
        if (angle > 180f)
            angle -= 360f;
        else if (angle < -181f)
            angle += 360f;
        return angle;
    }
}
