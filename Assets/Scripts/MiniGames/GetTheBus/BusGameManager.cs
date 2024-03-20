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

    //bus stop
    [SerializeField] BusStop BusStop;

    // Start is called before the first frame update
    void Start()
    {
        // move to awake
        _mAverageSpawnRate = GameManager.instance.Speed / 15;
        StartCoroutine(SpawnBus()); //

        BusStop.triggerEnter = BusStartOverride;
        BusStop.triggerExit = BusStopOverride;
    }

    // Update is called once per frame
    void Update()
    {
        if (_mTimer.timerValue == 0)
            EndGame(false);
        if (_mIsPaused) {
            if (_mCatch)
                EndGame(true);
            else
                EndGame(false);
        }
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
        while (!_mIsPaused) {
            float nextDelay = Random.Range(0, _mAverageSpawnRate);
            yield return new WaitForSeconds(nextDelay);
            GameObject bus = BusPool.SharedInstance.GetPooledBus(); 
            if (bus != null) {
                bus.transform.position = SpawnPosition;
                bus.SetActive(true);
            }
        }
    }

    void BusStartOverride(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Bus"))
            _mCatch = true;
    }

    void BusStopOverride(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Bus"))
            _mCatch = false;
    }
}
