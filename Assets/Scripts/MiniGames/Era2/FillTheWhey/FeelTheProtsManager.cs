using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FeelTheProtsManager : MiniGameManager
{
    [SerializeField] private FeelTheProtsInteractableManager _interactableManager;

    private void Start()
    {
        _interactableManager.OnEndGame += HandleEndGame;
        _interactableManager.Bars.transform.position = new Vector3(_interactableManager.Bars.transform.position.x, 
            Random.Range(-0.2f, -1.6f));
        _interactableManager.Bars.transform.localScale = new Vector3(_interactableManager.Bars.transform.localScale.x, 
            2.5f - (float)GameManager.instance.FasterLevel / 10 + 0.2f);
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
