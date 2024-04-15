using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionBar : MonoBehaviour
{
    float maxBarWidth = -925;
    public float maxBarValue = 2000;
    public float depletionRate = 200; // Rate at which the bar depletes per second
    public float barValue;
    [SerializeField] public RectTransform Fill;
    [SerializeField] int StartingValue;



    void Start()
    {
        float StartingPos = maxBarWidth * StartingValue / maxBarValue;
        Fill.anchoredPosition = new Vector2(StartingPos, Fill.anchoredPosition.y);
    }

    void Update()
    {
        if(barValue != 0)
        {
            float depletion = (depletionRate + (GameManager.instance.FasterLevel * 50)) * Time.deltaTime;
            barValue += depletion;
            barValue = Mathf.Clamp(barValue, 0f, maxBarValue);
            UpdateUI();
        }
    }

    public void AddValue(float value)
    {
        if (barValue != 0)
        {
            barValue -= value;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        float newX = barValue * maxBarWidth / maxBarValue;
        Fill.anchoredPosition = new Vector2(newX, Fill.anchoredPosition.y);
    }

}
