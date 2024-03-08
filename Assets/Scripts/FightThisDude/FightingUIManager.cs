using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightingUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public RectTransform Panel;

    [SerializeField] int StartingSliderWidth;

    int maxBarWidth = 725;
    int maxBarValue = 2000;

    int barValue;

    private void Start()
    {
       
        barValue = StartingSliderWidth * maxBarValue / maxBarWidth;
        Panel.sizeDelta = new Vector2(StartingSliderWidth, Panel.sizeDelta.y);

    }

    public void ChangeSlider(int value)
    {
        barValue += value;
        int newWidth = barValue * maxBarWidth / maxBarValue;
        Panel.sizeDelta = new Vector2(newWidth, Panel.sizeDelta.y);

    }

    public void ReduceSlider(int value)
    {
        barValue -= value;
        int newWidth = barValue * maxBarWidth / maxBarValue;
        Panel.sizeDelta = new Vector2(newWidth, Panel.sizeDelta.y);
    }

}
