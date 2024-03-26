using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class BusGameManager : MiniGameManager
{
    bool _mIsPaused = false;
    bool _mCatch = false;    

    // bus Spawn
    [SerializeField] Vector3 SpawnPosition;
    private float _mAverageSpawnRate;
    private float _mBusNumber;

    //bus stop
    [SerializeField] BusStop BusStop;

    // Start is called before the first frame update
    void Start()
    {
        _mAverageSpawnRate = GameManager.instance.Speed / 2;
        StartCoroutine(SpawnBus());
        BusStop.triggerEnter = BusStartOverride;
        BusStop.triggerExit = BusStopOverride;
        _mBusNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_mIsPaused)
            EndGame(_mCatch);
        if (_mTimer.timerValue == 0)
            EndGame(false);
    }

    void EndGame(bool isWin)
    {
        EndMiniGame(isWin, miniGameScore);
        BusPool.SharedInstance.HideAllBuses();
    }

    public void OnClicked()
    {
        _mIsPaused = true;    
        BusPool.SharedInstance.StopAllBuses();
        StopCoroutine(SpawnBus());
    }

    IEnumerator  SpawnBus()
    {
        while (!_mIsPaused && _mBusNumber < 4) {
            float nextDelay = Random.Range(_mAverageSpawnRate / 4, _mAverageSpawnRate);
            yield return new WaitForSeconds(nextDelay);
            GameObject bus = BusPool.SharedInstance.GetPooledBus(); 
            if (bus != null) {
                bus.transform.position = SpawnPosition;
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
