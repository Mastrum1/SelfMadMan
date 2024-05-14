using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CleanYourYatchGameManager : MiniGameManager
{
    private List<GameObject> _mPooledObjects;
    [SerializeField] private List<GameObject> _mObjectsToPool;
    [SerializeField] private int _mAmountToPool;
    [SerializeField] private YatchHandler _mYatchHandler;
    [SerializeField] private List<GameObject> _mSpawnPoints;
    [SerializeField] private Transform _mParent;
    private bool _mEnd;
    private int _mGarbageRemaining;

    void Start()
    {
        _mGarbageRemaining = _mAmountToPool;
        _mEnd = false;
        _mYatchHandler.GarbageDeleted += OnGarbageDeleted;
        GameObject mTmp;
        _mPooledObjects = new List<GameObject>();
        for (int i = 0, n = 0; i < _mAmountToPool; i++, n++) {
            if (n == _mObjectsToPool.Count)
                n = 0;
            mTmp = Instantiate(_mObjectsToPool[n], RandomPointInBox(), Quaternion.identity, transform);
            _mPooledObjects.Add(mTmp);
        }
    }

    void DisableGarbage()
    {
        for (int i = 0, n = 0; i < _mPooledObjects.Count; i++, n++)
            _mPooledObjects[i].GetComponent<GarbageDrag>().Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (_mEnd)
            return;
        if (_mGarbageRemaining == 0)
            EndGame(true);
        base.Update();
        if (!_gameIsPlaying)
            DisableGarbage();
    }

    void EndGame(bool status)
    {
        if (!_mEnd) {
            DisableGarbage();
            EndMiniGame(status, miniGameScore);
            _mEnd = true;
        }
    }

    private Vector3 RandomPointInBox()
    {
        return new Vector3(
            Random.Range( _mSpawnPoints[0].transform.position.x, _mSpawnPoints[1].transform.position.x),
            Random.Range(_mSpawnPoints[0].transform.position.y, _mSpawnPoints[2].transform.position.y),
           0
        );
    }


    void OnGarbageDeleted()
    {
        _mGarbageRemaining --;
    }

    void OnDestroy()
    {
        _mYatchHandler.GarbageDeleted -= OnGarbageDeleted;
    }
}