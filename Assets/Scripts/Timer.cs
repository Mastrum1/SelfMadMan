using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.UIElements;



public class Timer : MonoBehaviour
{

    public event Action OnTimerEnded;
    [SerializeField] public RectMask2D RectMask2D;
    [SerializeField] public RectTransform Square;

    public int TimerValue;
    public float totalTimeInSeconds = 10f; // Total time to reach the max padding
    private float elapsedTime = 0f; // Elapsed time since start
    private bool _timerOn;

    public Vector3 startPosition;
    public Vector3 targetPosition;

    public bool MyTimer { get => _timerOn;  set => _timerOn = value; }

    public void Start()
    {
        startPosition = Square.anchoredPosition;
        targetPosition = new Vector2(-400f, Square.anchoredPosition.y);
    }
    public void ResetTimer(float time)
    {
        TimerValue = (int)time;
        _timerOn = true;
        totalTimeInSeconds = time;
    }

    void Update()
    {
        if(_timerOn)
        {
            elapsedTime += Time.deltaTime;
            TimerValue = (int)totalTimeInSeconds - (int)elapsedTime;
            float newPadding = Mathf.Lerp(0, 1080, elapsedTime / totalTimeInSeconds);
            RectMask2D.padding = new Vector4(RectMask2D.padding.x, RectMask2D.padding.y, newPadding, RectMask2D.padding.w);
            Square.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / totalTimeInSeconds);
        }
     
    }

}
