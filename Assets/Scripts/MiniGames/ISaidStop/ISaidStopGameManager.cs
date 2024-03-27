using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISaidStopGameManager : MiniGameManager
{
    // Start is called before the first frame update
    [SerializeField] HandMovement Hand;

    void Start()
    {
        Hand.CigarPackCaught +=  OnPackCaught;
    }

    // Update is called once per frame
    void Update()
    {
        if (_mTimer.timerValue == 0)
            EndGame(true);
    }

    void EndGame(bool isWin) 
    {
        EndMiniGame(isWin, miniGameScore);
        Hand.Stop();
    }

    void OnPackCaught()
    {
        EndGame(false);
    }

    void OnDestroy()
    {
        Hand.CigarPackCaught += OnPackCaught;
    }
}
