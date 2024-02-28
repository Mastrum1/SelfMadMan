using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingGameManager : MiniGameManager
{
    [SerializeField] SleepingUIManager UIManager;

    int interval = 150;
   
    public void Awake()
    {
        end = false;
        win = false;
        GameManager.instance.SelectNewMiniGame(this);
    }

    public void OnClicked()
    {
        UIManager.ChangeSlider();
    }

    private void Update()
    {
        if (UIManager.Slider.value == 100)
        {
            win = true;
            end = true;
            EndMiniGame(win, miniGameScore);
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
