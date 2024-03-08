using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingGame : MiniGameManager
{
    [SerializeField] FightingUIManager UIManager;




    int interval = 0;
   
    public void Awake()
    {
        _mTimer.ResetTimer(10f);
    }

    public void OnClicked()
    {
        UIManager.ChangeSlider(150);
    }

    private void Update()
    {
       
        if (_mTimer.CurrentTime == 0)
        {
            Debug.Log("Time's up");
            EndMiniGame(false, miniGameScore);
        }
        if (UIManager.Panel.rect.width >= 725)
        {
            Debug.Log("Game finished");
            EndMiniGame(true, miniGameScore);
        }
        else
        {
            _mTimer.UpdateTimer();
            if (interval == 0)
            {
                if (UIManager.Panel.rect.width >= 0)
                {
                    UIManager.ReduceSlider(20);
                }
                interval = 20;
            }
            else interval--;
        }
      
     
    }


}
