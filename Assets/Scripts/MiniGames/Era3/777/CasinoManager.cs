using CW.Common;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CasinoManager : MiniGameManager
{
    [SerializeField] private CasinoInteractableManager _mInteractableManager;

    private void Start()
    {
        _mInteractableManager.OnGameEnd += OnGameEnd;
    }

    void OnGameEnd(bool win)
    {
        EndMiniGame(win, miniGameScore);
    }

    private void OnDisable()
    {
        _mInteractableManager.OnGameEnd -= OnGameEnd;
    }
}
