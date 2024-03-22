using UnityEngine;

public class DirtyRoadManager : MiniGameManager
{
    [SerializeField] private int _numOfRoads;
    [SerializeField] private DirtyRoadInteractableManager _interactableManager;
    [SerializeField] private GameObject _roadParent;
    [SerializeField] private GameObject _road;
    public float miniGameTime;

    public override void Awake()
    {
        base.Awake();
        
        _interactableManager.OnGameEnd += OnGameEnd;
        SpawnRoads();
    }

    void OnGameEnd(bool win)
    {
        EndMiniGame(win, miniGameScore);
    }

    void SpawnRoads()
    {
        if (_numOfRoads is < 2 or > 6)
        {
            Debug.LogError("number of roads is < 2 or > 6");
            return;
        }
        
        for (int i = 0; i < _numOfRoads; i++)
        {
            var gap = (6.5f / (_numOfRoads + 1)) * (i+1);
            Debug.Log(gap);
            var road = Instantiate(_road, new Vector3(0,3.5f - gap, 0), Quaternion.identity);
            road.transform.SetParent(_roadParent.transform);
        }
    }

    private void OnDestroy()
    {
        _interactableManager.OnGameEnd -= OnGameEnd;
    }
}
