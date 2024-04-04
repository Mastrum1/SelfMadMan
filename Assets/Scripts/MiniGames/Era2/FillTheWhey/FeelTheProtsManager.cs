using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FeelTheProtsManager : MiniGameManager
{
    [SerializeField] private FeelTheProtsInteractableManager _interactableManager;

    private void Start()
    {
        _interactableManager.OnEndGame += HandleEndGame;
        _interactableManager.Bars.transform.position = new Vector3(_interactableManager.Bars.transform.position.x, Random.Range(-0.2f, -1.6f));
        // decrease the scale of the bar with the current difficulty level
    }

    private void HandleEndGame(bool win)
    {
        EndMiniGame(win, miniGameScore);
    }

    private void OnDestroy()
    {
        _interactableManager.OnEndGame -= HandleEndGame;
    }
}
