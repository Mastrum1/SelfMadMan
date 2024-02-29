using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;



public class Timer : MonoBehaviour
{
    public float CurrentTime;
    private float _mMaxTime;
    [SerializeField] private Slider _mTimerSlider;

    private void Update()
    {
        _mTimerSlider.value =  CurrentTime * _mTimerSlider.maxValue / _mMaxTime;
    }

    public void UpdateTimer()
    {
        if (CurrentTime > 0f)
        {
            CurrentTime -= Time.deltaTime;
            if (CurrentTime <= 0f)
            {
                CurrentTime = 0f;
            }
        }
    }

    public void ResetTimer(float time)
    {
        _mMaxTime = time;
        CurrentTime = time;

    }
}