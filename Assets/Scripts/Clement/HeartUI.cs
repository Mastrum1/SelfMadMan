using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    [SerializeField] public Slider slider;

    public void ReduceSlider()
    {
        slider.value--;
    }
}
