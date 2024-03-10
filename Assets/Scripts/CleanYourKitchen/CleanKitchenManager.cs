using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanKitchenManager : MiniGameManager
{
    public float miniGameTime;
    // Start is called before the first frame update
    private void Awake()
    {
        _mTimer.ResetTimer(miniGameTime);
    }

    // Update is called once per frame
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
