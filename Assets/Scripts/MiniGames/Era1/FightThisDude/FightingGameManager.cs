using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingGame : MiniGameManager
{
    [SerializeField] private FightingUIManager _UIManager;

    public void OnClicked()
    {
        if(_gameIsPlaying)
        {
            _UIManager.OnFightImageChange();
            _UIManager.Bar.AddValue(150 - (GameManager.instance.FasterLevel * 20));
            Debug.Log("Clicked");
        }
    }

    public override void Update()
    {
        base.Update();
        if(_gameIsPlaying)
        {
            if (_UIManager.Bar.barValue == 0)
            {
                Amount++;
                EndMiniGame(true, miniGameScore);
            }
            if (_UIManager.Bar.barValue == _UIManager.Bar.maxBarValue)
            {
                EndMiniGame(false, miniGameScore);
            }
        }
    }
}
