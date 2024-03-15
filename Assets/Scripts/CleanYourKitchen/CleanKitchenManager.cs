using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanKitchenManager : MiniGameManager
{
    // Update is called once per frame
    void Update()
    {
        if (_mTimer.timerValue == 0)
        {
            Debug.Log("Time's up");
            EndMiniGame(false, miniGameScore);
        }
    }
}
