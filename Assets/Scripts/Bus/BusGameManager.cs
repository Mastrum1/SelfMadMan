using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class BusGameManager : MiniGameManager
{
    [SerializeField] float TimeRemaining = 10.0f;
    bool _mIsPaused = false;

    bool _mCatch = false;    

    // bus Spawn
    [SerializeField] private Vector3 SpawnPosition;
    private float _mAverageSpawnRate;

    //bus stop
    [SerializeField] BusStop BusStop;

    public void Awake()
    {
        _mTimer.ResetTimer(TimeRemaining);
        _mAverageSpawnRate = TimeRemaining / 15;
        StartCoroutine(SpawnBus());
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        BusStop.triggerEnter = BusStartOverride;
        BusStop.triggerExit = BusStopOverride;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_mTimer.timerValue == 0) {
            Debug.Log("Time's up");
            EndMiniGame(false, miniGameScore);
        }
        if (_mIsPaused) {
            if (_mCatch) {
                Debug.Log("Win");
                EndMiniGame(true, miniGameScore);
            } else {
                Debug.Log("Loose");
                EndMiniGame(false, miniGameScore);
            }
        }
        if ( !_mIsPaused && Input.GetMouseButtonDown(0)) {
            _mIsPaused = true;
            BusPool.SharedInstance.StopAllBuses();
            StopCoroutine(SpawnBus());
        }
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
        if (collider2D.gameObject.CompareTag("Bus")) {
            _mCatch = true;
        }
    }

    void BusStopOverride(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Bus")) {
            _mCatch = false;
        }
    }
}
