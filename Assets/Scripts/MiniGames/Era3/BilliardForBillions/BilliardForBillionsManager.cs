using UnityEngine;

public class BilliardForBillionsManager : MiniGameManager
{
    [SerializeField] private BilliardForBillionsInteractableManager _interactableManager;
    private void Start()
    {
        _interactableManager.OnWin += EndGame;
    }

    private void EndGame(bool win)
    {
        EndMiniGame(win, miniGameScore);
    }

    private void OnDestroy()
    {
        _interactableManager.OnWin -= EndGame;
    }
}
