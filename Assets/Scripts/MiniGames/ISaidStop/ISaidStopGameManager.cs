using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISaidStopGameManager : MiniGameManager
{
    // Start is called before the first frame update
    [SerializeField] HandMovement Hand;

    void Start()
    {
        base.Start();
        Hand.PackCaught = OnPackCaught;
    }

    // Update is called once per frame
    void Update()
    {
        //if (_mTimer.timerValue == 0)
          //  EndMiniGame(true, miniGameScore);
    }

    void OnPackCaught()
    {
        //EndMiniGame(false, miniGameScore);
    }
}
