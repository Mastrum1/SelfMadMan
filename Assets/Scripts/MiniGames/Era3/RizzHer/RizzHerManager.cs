using CW.Common;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RizzHerManager : MiniGameManager
{
    [SerializeField] private RizzHerInteractableManager _mInteractableManager;

    [SerializeField] private List<GameObject> _leftObj;
    [SerializeField] private List<GameObject> _midObj;
    [SerializeField] private List<GameObject> _rightObj;

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
