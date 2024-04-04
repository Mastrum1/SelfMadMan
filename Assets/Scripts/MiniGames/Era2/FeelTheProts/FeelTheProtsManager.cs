using System;
using UnityEngine;

public class FeelTheProtsManager : MiniGameManager
{
    [SerializeField] private FeelTheProtsInteractableManager _interactableManager;
    void Start()
    {
        _interactableManager.OnEndGame += HandleEndGame;
    }

    void HandleEndGame(bool win)
    {
        EndMiniGame(win, miniGameScore);
    }

    private void OnDestroy()
    {
        _interactableManager.OnEndGame -= HandleEndGame;
    }
}
