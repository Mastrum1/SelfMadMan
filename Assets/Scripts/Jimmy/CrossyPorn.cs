using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossyRoad : MiniGameManager
{
    [SerializeField] private GameObject _mPlayer;
    public float miniGameTime;
    private void Awake()
    {
        _mTimer.ResetTimer(miniGameTime);
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        if (_mTimer.CurrentTime == 0)
        {
            Debug.Log("Time's up");
            EndMiniGame(false, miniGameScore);
        }
        else
        {
            _mTimer.UpdateTimer();
        }
    }
}
