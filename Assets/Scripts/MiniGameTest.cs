using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTest : MonoBehaviour
{
    private Scoring _scoring;
    private float _mCurrentTime;
    private float _mCurrentScore;
    void Start()
    {
        _mCurrentTime = 0;
        _mCurrentScore = 1;
        _scoring = new Scoring();
    }
    
    void Update()
    {
        _mCurrentTime += Time.deltaTime;

        if (_mCurrentTime > 4)
        {
            _mCurrentScore = _scoring.ChangeScore(Scoring.Param.Add, _mCurrentScore, 0.4f);
            _mCurrentTime = 0;
            print(_mCurrentScore);
        }
    }
}
