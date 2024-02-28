using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingGameManager : MonoBehaviour
{
    [SerializeField] SleepingUIManager UIManager;

    int interval = 150;
    bool win = false;

    public void OnClicked()
    {
        UIManager.ChangeSlider();
    }

    private void Update()
    {
        if (UIManager.Slider.value == 100)
        {
            win = true;
        }
        else
        {
            if (interval == 0)
            {
                if (UIManager.Slider.value != 0)
                {
                    UIManager.ReduceSlider(2);
                }
                interval = 60;
            }
            else interval--;
        }
      
     
    }


}
