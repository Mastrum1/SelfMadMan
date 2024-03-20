using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class BusGameManager : MiniGameManager
{
<<<<<<<< HEAD:Assets/Scripts/Bus/BusGameManager.cs
    [SerializeField] float TimeRemaining = 10.0f;
========
>>>>>>>> fix/minigames:Assets/Scripts/MiniGames/GetTheBus/BusGameManager.cs
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
<<<<<<<< HEAD:Assets/Scripts/Bus/BusGameManager.cs
        
        if (_mTimer.timerValue == 0) {
            Debug.Log("Time's up");
            EndMiniGame(false, miniGameScore);
        }
========
        if (_mTimer.timerValue == 0)
            EndMiniGame(false, miniGameScore);
>>>>>>>> fix/minigames:Assets/Scripts/MiniGames/GetTheBus/BusGameManager.cs
        if (_mIsPaused) {
            if (_mCatch)
                EndMiniGame(true, miniGameScore);
<<<<<<<< HEAD:Assets/Scripts/Bus/BusGameManager.cs
            } else {
                Debug.Log("Loose");
                EndMiniGame(false, miniGameScore);
            }
        }
        if ( !_mIsPaused && Input.GetMouseButtonDown(0)) {
            _mIsPaused = true;
            BusPool.SharedInstance.StopAllBuses();
            StopCoroutine(SpawnBus());
========
            else
                EndMiniGame(false, miniGameScore);
>>>>>>>> fix/minigames:Assets/Scripts/MiniGames/GetTheBus/BusGameManager.cs
        }
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
