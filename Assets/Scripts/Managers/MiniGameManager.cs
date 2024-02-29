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


    public void EndMiniGame(bool won, int score)
    {
        // Trigger the event
        OnMiniGameEnd?.Invoke(won, score);
    }
}
