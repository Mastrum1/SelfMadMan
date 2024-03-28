using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanYourYatchGameManager : MiniGameManager
{
    private List<GameObject> _mPooledObjects;
    [SerializeField] private List<GameObject> _mObjectsToPool;
    [SerializeField] private int _mAmountToPool;
    [SerializeField] private Collider2D _mCollider;
    [SerializeField] private InputManager _mInputManager;
    [SerializeField] private YatchHandler _mYatchHandler;
    private int _mGarbageRemaining;

    void Start()
    {
        _mGarbageRemaining = _mAmountToPool;
        _mYatchHandler.GarbageDeleted += OnGarbageDeleted;
        GameObject mTmp;
        _mPooledObjects = new List<GameObject>();
        for (int i = 0, n = 0; i < _mAmountToPool; i++, n++) {
            if (n == _mObjectsToPool.Count)
                n = 0;
            mTmp = Instantiate(_mObjectsToPool[n]);
            mTmp.transform.position = RandomPointInBox();
            _mPooledObjects.Add(mTmp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_mGarbageRemaining == 0)
            EndMiniGame(true, miniGameScore);
        if (_mTimer.timerValue == 0)
            EndMiniGame(false, miniGameScore);
    }

    private Vector3 RandomPointInBox()
    {
        return _mCollider.bounds.center + new Vector3(
           (Random.value - 0.5f) * _mCollider.bounds.size.x,
           (Random.value - 0.5f) * _mCollider.bounds.size.y,
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
