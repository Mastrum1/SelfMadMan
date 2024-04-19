using UnityEngine;

public class DirtyRoadManager : MiniGameManager
{
    [SerializeField] private DirtyRoadInteractableManager _interactableManager;

    private int _numOfAds;

    private void Start()
    {
        _interactableManager.OnGameEnd += OnGameEnd;
        
        _numOfAds = GameManager.instance.FasterLevel + 2;
        if (_numOfAds > 8) _numOfAds = 8;
        
        _interactableManager.EnableAds(_numOfAds);
    }

    private void OnGameEnd(bool win)
    {
        if (win)
        {
            Amount++;
        }
        EndMiniGame(win, miniGameScore);
    }

    private void OnDestroy()
    {
        _interactableManager.OnGameEnd -= OnGameEnd;
    }
}
