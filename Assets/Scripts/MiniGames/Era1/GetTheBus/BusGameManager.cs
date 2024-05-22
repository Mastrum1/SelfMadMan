using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class BusGameManager : MiniGameManager
{
    bool _mIsPaused = false;
    bool _mCatch = false;
    bool _mIsEnd;
    private AudioManager _audioManager;
    // bus Spawn
    [SerializeField] Vector3 _mSpawnPosition;
    private float _mAverageSpawnRate;
    private float _mBusNumber;

    [SerializeField] private RectTransform _PauseButton;

    [SerializeField] EnterScreen _mEnterScreen;
    [SerializeField] ExitScreen _mExitScreen;

    void Start()
    {
        _audioManager = AudioManager.Instance;
        _mAverageSpawnRate = GameManager.instance.Speed / 2;
        StartCoroutine(SpawnBus());
        _mEnterScreen.triggerEnter = BusStartOverride;
        _mExitScreen.triggerExit = BusStopOverride;
        _mBusNumber = 0;
        _mIsEnd  = false;
    }

    private void Update()
    {
        base.Update();
        
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

    public void OnClicked(Vector3 fingerPos)
    {
        Vector3 PauseButtonPos = RectTransformUtility.WorldToScreenPoint(Camera.main, _PauseButton.position);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(PauseButtonPos.x, PauseButtonPos.y, 0));
        if (fingerPos.x > worldPos.x - 0.6f && fingerPos.x < worldPos.x + 0.6f && fingerPos.y < worldPos.y + 0.6f && fingerPos.y > worldPos.y - 0.6f)
            return;
        else
        {
            _audioManager.StopSFX();
        _audioManager.PlaySFX(1);
            _mIsPaused = true;
            //BusPool.SharedInstance.HideAllBuses();
            BusPool.SharedInstance.StopAllBuses();
            StopCoroutine(SpawnBus());
        }
    }

    IEnumerator  SpawnBus()
    {
        _audioManager.PlaySFX(0);
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
        Amount++;
    }

    void BusStopOverride(Collider2D collider2D)
    {
        _mCatch = false;
    }
}
