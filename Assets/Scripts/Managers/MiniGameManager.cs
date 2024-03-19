using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameManager : MonoBehaviour
{
    public delegate void MiniGameEndHandler(bool won, int score);
    public event MiniGameEndHandler OnMiniGameEnd;

    [SerializeField] public Timer _mTimer;

    public int miniGameScore;

    public void Awake()
    {
        _mTimer.ResetTimer(GameManager.instance.Speed);
    }
    public void Start()
    {
        _mTimer.ResetTimer(GameManager.instance.Speed);
        GameManager.instance.SelectNewMiniGame(this);
    }

    public void EndMiniGame(bool won, int score)
    {
        
        Debug.Log("finished");
        OnMiniGameEnd?.Invoke(won, score);
    }
}
