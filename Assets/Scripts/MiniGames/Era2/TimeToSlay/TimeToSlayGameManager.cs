using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToSlayGameManager : MiniGameManager
{
    [SerializeField] MaskSpawner _mMaskSpawner;
    [SerializeField] BrushMovement _mBrushMovement;
    [SerializeField] GameObject _mFoam;
    private bool _mIsEnd;

    void Start()
    {
        _mMaskSpawner.OnCompleted += OnCompleted;
        _mIsEnd = false;
    }
    

    void Update()
    {
        if (_mIsEnd)
            return;
        if (_mTimer.TimerValue == 0)
           EndGame(false);
    }

    void EndGame(bool status)
    {
        if (!_mIsEnd) {
            _mIsEnd = true;
            _mBrushMovement.Stop();
            EndMiniGame(status, miniGameScore);
        }
    }

    void OnCompleted()
    {
        _mFoam.SetActive(true);
        StartCoroutine(Win());
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(0.5f);
        EndGame(true);
    }

    void OnDestroy()
    {
        _mMaskSpawner.OnCompleted -= OnCompleted; 
    }

}
