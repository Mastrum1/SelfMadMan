using CW.Common;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FindYourChadManager : MiniGameManager
{
    [SerializeField] private FindYourChadInteractableManager _mInteractableManager;

    private void Start()
    {
        _mInteractableManager.GameEnd += OnGameEnd;
    }

    void OnGameEnd(bool win)
    {
        EndMiniGame(win, miniGameScore);
    }

    private void OnDisable()
    {
        _mInteractableManager.GameEnd -= OnGameEnd;
    }
}
