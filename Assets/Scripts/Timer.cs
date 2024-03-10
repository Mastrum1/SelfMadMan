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



    public void ResetTimer(float time)
    {
        timerValue = maxBarValue;
        _mDepletionRate = maxBarValue / time;
        Panel.sizeDelta = new Vector2(maxBarWidth, Panel.sizeDelta.y);
    }

    void Update()
    {
        if (timerValue > 0)
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
