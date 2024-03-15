using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScene : MonoBehaviour
{
    [SerializeField] public Slider slider;

    private void Awake()
    {
        ReduceSlider();
    }

    public void ReduceSlider()
    {
        slider.value = GameManager.instance.GetHearts();
    }
}
