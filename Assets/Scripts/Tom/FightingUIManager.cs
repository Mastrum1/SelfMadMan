using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightingUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Slider Slider;

    public void ChangeSlider()
    {
        Slider.value += 10; //value will change according to difficulty
    }

    public void ReduceSlider(int value)
    {
        Slider.value -= value;
    }

}
