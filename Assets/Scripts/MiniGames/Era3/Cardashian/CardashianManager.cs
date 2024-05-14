using CW.Common;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardiashianManager : MiniGameManager
{
    [SerializeField] private CardashianInteractableManager _mInteractableManager;

    private float _Space = 1.78f;

    [SerializeField] private List<cardashianCard> _mObj;

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
