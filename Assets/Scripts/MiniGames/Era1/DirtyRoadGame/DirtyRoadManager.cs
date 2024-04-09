using UnityEngine;

public class DirtyRoadManager : MiniGameManager
{
    [SerializeField] private DirtyRoadInteractableManager _interactableManager;
    [SerializeField] private GameObject _dirtyAd;
    private int _numOfAds;

    private void Start()
    {
        _interactableManager.OnGameEnd += OnGameEnd;
        _interactableManager.OnSpawnMoreAds += SpawnAds;
        _numOfAds = GameManager.instance.FasterLevel + 2;
        _interactableManager.EnableAds(_numOfAds);
    }

    private void SpawnAds(int numToSpawn)
    {
        Instantiate(_dirtyAd);
    }

    void OnGameEnd(bool win)
    {
        EndMiniGame(win, miniGameScore);
    }

    private void OnDestroy()
    {
        _interactableManager.OnGameEnd -= OnGameEnd;
    }
}
