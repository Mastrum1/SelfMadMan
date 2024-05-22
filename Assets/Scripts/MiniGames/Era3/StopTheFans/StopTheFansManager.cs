using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class StopTheFansManager : MiniGameManager
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> _hands;
    [SerializeField] StopTheFansInteractablesManager _interactableManager;
    private bool _mIsEnd;

    void Start()
    {
        _mIsEnd = false;
        _interactableManager.OnJamesTouched += OnJamesTouched;
    }

    // Update is called once per frame
    void Update()
    {
        if (_mIsEnd)
            return;
        if (_mTimer.TimerValue == 0 && !_mIsEnd && _gameIsPlaying)
            EndGame(true);
    }

    void EndGame(bool isWin)
    {
        if (!_mIsEnd)
        {
            _interactableManager.StopAllHands();
            Amount++;
            EndMiniGame(isWin, miniGameScore);
            _mIsEnd = true;
        }
    }

    void OnJamesTouched()
    {
        if (!_mIsEnd)
            EndGame(false);
    }

    void OnDestroy()
    {
        _interactableManager.OnJamesTouched += OnJamesTouched;
    }
}
