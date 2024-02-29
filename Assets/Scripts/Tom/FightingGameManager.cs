using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingGame : MiniGameManager
{
    [SerializeField] FightingUIManager UIManager;

    int interval = 150;
   
    public void Awake()
    {
        _mTimer.ResetTimer(10f);
    }

    public void OnClicked()
    {
        UIManager.ChangeSlider();
    }

    private void Update()
    {
       
        if (_mTimer.CurrentTime == 0)
        {
            Debug.Log("Time's up");
            EndMiniGame(false, miniGameScore);
        }
        if (UIManager.Slider.value == 100)
        {
            Debug.Log("Game finished");
            EndMiniGame(true, miniGameScore);
        }
        else
        {
            _mTimer.UpdateTimer();
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
