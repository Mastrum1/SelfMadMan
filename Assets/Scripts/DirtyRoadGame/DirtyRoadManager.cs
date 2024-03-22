using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DirtyRoadManager : MiniGameManager
{
    private int _numOfRoads;
    
    [SerializeField] private int mNumOfAdds;
    [SerializeField] private OnAddsCollide _onAddsCollide;
    [SerializeField] private GameObject _roadParent;
    [SerializeField] private GameObject _road;
    public float miniGameTime;

    public override void Awake()
    {
        base.Awake();
        
        _onAddsCollide.OnCollided += OnGameEnd;
    }

    private void Start()
    {
        _numOfRoads = 2;
        SpawnRoads();
    }

    void OnGameEnd(bool win)
    {
        EndMiniGame(win, miniGameScore);
    }

    void SpawnRoads()
    {
        for (int i = 0; i < _numOfRoads; i++)
        {
            var gap = (5 / (_numOfRoads + 1)) * (i+1);
            Debug.Log(gap);
            var road = Instantiate(_road, new Vector3(0,-2.5f + gap, 0), Quaternion.identity);
            road.transform.SetParent(_roadParent.transform);
        }
    }

    private void OnDestroy()
    {
        _onAddsCollide.OnCollided -= OnGameEnd;
    }
}
