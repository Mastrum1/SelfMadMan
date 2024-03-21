using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DirtyRoadManager : MiniGameManager
{
    public enum RoadStates
    {
        Two,
        Three,
        Four,
        Five,
        Six
    }
    
    private RoadStates _roadState;

    [SerializeField] private Dictionary<RoadStates, float> _roadGaps;
    [SerializeField] private int mNumOfAdds;
    [SerializeField] private OnAddsCollide _onAddsCollide;
    [SerializeField] private GameObject _road;
    public float miniGameTime;

    public override void Awake()
    {
        base.Awake();
        
        _onAddsCollide.OnCollided += OnGameEnd;
    }

    private void Start()
    {
        ChangeRoadState(Random.Range(0, Enum.GetNames(typeof(RoadStates)).Length));
        SpawnRoads(_roadState);
    }

    void OnGameEnd(bool win)
    {
        EndMiniGame(win, miniGameScore);
    }

    void ChangeRoadState(int state)
    {
        _roadState = (RoadStates)state;
    }

    void SpawnRoads(RoadStates state)
    {
        for (int i = 0; i < (int)state; i++)
        {
            Instantiate(_road, new Vector3(0,_roadGaps[state], 0), Quaternion.identity);
        }
    }

    private void OnDestroy()
    {
        _onAddsCollide.OnCollided -= OnGameEnd;
    }
}
