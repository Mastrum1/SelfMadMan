using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FeelTheProtsManager : MiniGameManager
{
    [SerializeField] private FeelTheProtsInteractableManager _interactableManager;
    [SerializeField] private GameObject _milkParent;

    private void Start()
    {
        _interactableManager.OnEndGame += HandleEndGame;
        _interactableManager.Bars.transform.position = new Vector3(_interactableManager.Bars.transform.position.x, 
            Random.Range(-1.25f, -2.2f));
        _interactableManager.Bars.transform.localScale = new Vector3(_interactableManager.Bars.transform.localScale.x, 
            2.5f - (float)GameManager.instance.FasterLevel / 4);
    }

    private void HandleEndGame(bool win)
    {
        _milkParent.SetActive(false);
        if (win)
        {
            Amount++;
        }
        EndMiniGame(win, miniGameScore);
    }

    private void OnDestroy()
    {
        _interactableManager.OnEndGame -= HandleEndGame;
    }
}
