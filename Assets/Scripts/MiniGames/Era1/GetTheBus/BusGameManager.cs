using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class BusGameManager : MiniGameManager
{
    bool _mIsPaused = false;
    bool _mCatch = false;
    bool _mIsEnd;

    // bus Spawn
    [SerializeField] Vector3 _mSpawnPosition;
    private float _mAverageSpawnRate;
    private float _mBusNumber;

    [SerializeField] EnterScreen _mEnterScreen;
    [SerializeField] ExitScreen _mExitScreen;

    void Start()
    {
        _mAverageSpawnRate = GameManager.instance.Speed / 2;
        StartCoroutine(SpawnBus());
        _mEnterScreen.triggerEnter = BusStartOverride;
        _mExitScreen.triggerExit = BusStopOverride;
        _mBusNumber = 0;
        _mIsEnd  = false;
    }

    void Update()
    {
        if (_mIsEnd)
            return;
        if (_mIsPaused)
            EndGame(_mCatch);
    }

    void EndGame(bool isWin)
    {
        _mIsEnd = true;
        //BusPool.SharedInstance.HideAllBuses();
        EndMiniGame(isWin, miniGameScore);
    }

    public void OnClicked()
    {
        _mIsPaused = true;
        //BusPool.SharedInstance.HideAllBuses();
        BusPool.SharedInstance.StopAllBuses();
        StopCoroutine(SpawnBus());
    }

    IEnumerator  SpawnBus()
    {
        while (!_mIsPaused && _mBusNumber < 4) {
            float nextDelay = Random.Range(_mAverageSpawnRate / 2, _mAverageSpawnRate);
            yield return new WaitForSeconds(nextDelay);
            GameObject bus = BusPool.SharedInstance.GetPooledBus(); 
            if (bus != null) {
                bus.transform.position = _mSpawnPosition;
                bus.SetActive(true);
                _mBusNumber++;
            }
        }
    }

    void BusStartOverride(Collider2D collider2D)
    {
        _mCatch = true;
    }

    void BusStopOverride(Collider2D collider2D)
    {
        _mCatch = false;
    }
}
