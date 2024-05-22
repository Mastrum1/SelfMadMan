using System;
using UnityEngine;

public class GetTheFlourGameManager : MiniGameManager
{
    [SerializeField] private GetTheFlourInteractablemanager _interactablemanager;

    private void Start()
    {
        _interactablemanager.OnLoseGame += EndGame;
    }
    
    private void EndGame(bool win)
    {
        EndMiniGame(win, miniGameScore);
    }

    private void OnDestroy()
    {
        _interactablemanager.OnLoseGame -= EndGame;
    }
}
