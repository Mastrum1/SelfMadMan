using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.Video;

public class MiniGameManager : MonoBehaviour
{
    public delegate void MiniGameEndHandler(bool won, int score);
    public event MiniGameEndHandler OnMiniGameEnd;
    private bool _gameIsPlaying;

    [SerializeField] public Timer _mTimer;
    [SerializeField] private VideoPlayer _cash;
    [SerializeField] private Camera _sceneCamera;

    [SerializeField] private GameObject _loosePanel;

    [NonSerialized] public int miniGameScore;

    public virtual void Awake()
    {
        _gameIsPlaying = true;
        _cash.targetCamera = _sceneCamera;
        _mTimer.ResetTimer(GameManager.instance.Speed);
        GameManager.instance.SelectNewMiniGame(this);
    }

    public virtual void EndMiniGame(bool won, int score)
    {
        _gameIsPlaying = false;
        _mTimer.MyTimer = false;

        if (won)
            _cash.Play();
        else
            _loosePanel.SetActive(true);

        float timeout = won ? 1.5f : 0.5f;
        StartCoroutine(StartAnim(won, score, timeout));
    }

    IEnumerator StartAnim(bool won, int score, float timeout)
    {
        yield return new WaitForSeconds(timeout);

        if (_loosePanel.activeSelf)
            _loosePanel.SetActive(false);

        OnMiniGameEnd?.Invoke(won, score);
    }

    public virtual void Update()
    {
        if (_mTimer.timerValue == 0)
        {
            Debug.Log("Time's up");
            EndMiniGame(false, miniGameScore);
        }
    }
    
}
