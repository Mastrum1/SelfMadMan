using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingGame : MiniGameManager
{
    [SerializeField] private FightingUIManager _UIManager;

    public void OnClicked()
    {
        _UIManager.Bar.AddValue(150 - (GameManager.instance.FasterLevel * 20));
        Debug.Log("Clicked");
    }

    public override void Update()
    {
        base.Update();
        if(_gameIsPlaying)
        {
            if (_UIManager.Bar.barValue >= _UIManager.Bar.maxBarValue)
            {
                Debug.Log("Game finished");
                EndMiniGame(true, miniGameScore);
            }
            if (_UIManager.Bar.barValue == 0)
            {
                Debug.Log("Game finished");
                EndMiniGame(false, miniGameScore);
            }
        }

    }


}
