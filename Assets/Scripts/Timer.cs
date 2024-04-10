using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;



public class Timer : MonoBehaviour
{
    float maxBarWidth = 114;
    public float maxBarValue = 2000;
    private float _mDepletionRate;
    public float timerValue = 2;
    [SerializeField] public RectMask2D RectMask2D;
    [SerializeField] public RectTransform Square;

    public float totalTimeInSeconds = 10f; // Total time to reach the max padding
    private float elapsedTime = 0f; // Elapsed time since start
    float paddingAmount = 0;
    private bool _timerOn;

    public Vector3 startPosition;
    public Vector3 targetPosition;

    public bool MyTimer { get => _timerOn;  set => _timerOn = value; }

    public void Start()
    {
        startPosition = Square.anchoredPosition;
        targetPosition = new Vector2(-600f, Square.anchoredPosition.y);
    }
    public void ResetTimer(float time)
    {
    }

    void Update()
    {
        float newPadding = Mathf.Lerp(0, 1008, elapsedTime / totalTimeInSeconds);
        RectMask2D.padding = new Vector4(RectMask2D.padding.x, RectMask2D.padding.y, newPadding, RectMask2D.padding.w);
        Square.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / totalTimeInSeconds);
        elapsedTime += Time.deltaTime;
    }

}
