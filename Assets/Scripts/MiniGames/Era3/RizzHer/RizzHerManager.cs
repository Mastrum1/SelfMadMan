using CW.Common;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RizzHerManager : MiniGameManager
{
    [SerializeField] private RizzHerInteractableManager _mInteractableManager;

    [SerializeField] private List<BackGroundScrolling> _scrollingBackground;


    private void Start()
    {
        _mInteractableManager.GameEnd += OnGameEnd;
    }

    void OnGameEnd(bool win)
    {
        for (int i = 0; i < _scrollingBackground.Count; i++)
        {
            _scrollingBackground[i].Speed = 0;
        }
        EndMiniGame(win, miniGameScore);
    }

    public override void Update()
    {
        if (_mTimer.TimerValue <= GameManager.instance.Speed / 1.5)
            _mInteractableManager.EndGame();

        if (_mTimer.TimerValue == 0 && _gameIsPlaying)
            EndMiniGame(false, 0);


    }

    private void OnDisable()
    {
        _mInteractableManager.GameEnd -= OnGameEnd;
    }

}
