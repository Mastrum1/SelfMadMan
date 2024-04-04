using System;
using System.Collections.Generic;
using UnityEngine;

public class DirtyRoadManager : MiniGameManager
{
    [SerializeField] private DirtyRoadInteractableManager _interactableManager;
    [SerializeField] private GameObject _dirtyAd;
    [SerializeField] private int _numOfAds;

    private void Start()
    {
        _interactableManager.OnGameEnd += OnGameEnd;
        _interactableManager.OnSpawnMoreAds += SpawnAds;
        if (_numOfAds <= 0) _numOfAds = 4;
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
