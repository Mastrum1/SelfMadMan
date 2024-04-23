using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletionBar : MonoBehaviour
{
    float maxBarWidth = 725;
    public float maxBarValue = 2000;
    public float depletionRate = 100f; // Rate at which the bar depletes per second
    public float barValue;
    [SerializeField] public RectTransform Panel;
    [SerializeField] int StartingValue;



    void Start()
    {
        float StartingSliderWidth = maxBarWidth * StartingValue / maxBarValue;
        Panel.sizeDelta = new Vector2(StartingSliderWidth, Panel.sizeDelta.y);
    }

    void Update()
    {

        if(barValue < maxBarValue)
        {
            float depletion = depletionRate * Time.deltaTime;
            barValue -= depletion;
            barValue = Mathf.Clamp(barValue, 0f, maxBarValue);
            UpdateUI();
        }
    }

    public void AddValue(float value)
    {
        if (barValue < maxBarValue)
        {
            barValue += value;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        float newWidth = barValue * maxBarWidth / maxBarValue;
        Panel.sizeDelta = new Vector2(newWidth, Panel.sizeDelta.y);
    }

}
