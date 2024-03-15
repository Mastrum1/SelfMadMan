using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingGame : MiniGameManager
{
    [SerializeField] FightingUIManager UIManager;
    
    public void OnClicked()
    {
        UIManager.Bar.AddValue(150);
        Debug.Log("Clicked");
    }

    private void Update()
    {
       
        if (_mTimer.timerValue == 0 || UIManager.Bar.barValue <= 0)
        {
            Debug.Log("Time's up");
            EndMiniGame(false, miniGameScore);
        }
        if (UIManager.Bar.barValue >= UIManager.Bar.maxBarValue)
        {
            Debug.Log("Game finished");
            EndMiniGame(true, miniGameScore);
        } 
    }


}
