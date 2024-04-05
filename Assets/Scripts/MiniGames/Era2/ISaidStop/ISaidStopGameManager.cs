using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class ISaidStopGameManager : MiniGameManager
{
    // Start is called before the first frame update
    [SerializeField] HandMovement _mHand;
    private bool _mIsEnd;

    void Start()
    {
        _mIsEnd = false;
        _mHand.CigarPackCaught +=  OnPackCaught;
    }

    // Update is called once per frame
    void Update()
    {
        if (_mIsEnd)
            return;
        if (_mTimer.timerValue == 0)
            EndGame(true);
    }

    void EndGame(bool isWin) 
    {
        _mHand.Stop();
        EndMiniGame(isWin, miniGameScore);
        _mIsEnd = true;
    }

    void OnPackCaught()
    {
        EndGame(false);
    }

    void OnDestroy()
    {
        _mHand.CigarPackCaught -= OnPackCaught;
    }
}
