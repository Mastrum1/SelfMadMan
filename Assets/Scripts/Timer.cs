using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;



public class Timer : MonoBehaviour
{
    float maxBarWidth = 114;
    public float maxBarValue = 2000;
    private float _mDepletionRate;
    public float timerValue;
    [SerializeField] public RectTransform Panel;

    private bool _timerOn;
    public bool MyTimer { get => _timerOn;  set => _timerOn = value; }


    public void ResetTimer(float time)
    {
        MyTimer = true;
        timerValue = maxBarValue;
        _mDepletionRate = maxBarValue / time;
        Panel.sizeDelta = new Vector2(maxBarWidth, Panel.sizeDelta.y);
    }

    void Update()
    {
        if (timerValue > 0 && MyTimer)
        {
            float depletion = _mDepletionRate * Time.deltaTime;
            timerValue -= depletion;
            timerValue = Mathf.Clamp(timerValue, 0f, maxBarValue);
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        float newWidth = timerValue * maxBarWidth / maxBarValue;
        Panel.sizeDelta = new Vector2(newWidth, Panel.sizeDelta.y);
    }
}
