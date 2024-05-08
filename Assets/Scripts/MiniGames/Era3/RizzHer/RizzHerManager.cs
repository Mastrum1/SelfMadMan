using CW.Common;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RizzHerManager : MiniGameManager
{
    [SerializeField] private RizzHerInteractableManager _mInteractableManager;

    [SerializeField] private GameObject _mTableMaterial;

    private Renderer _mRenderer;

    private void Start()
    {
        _mInteractableManager.GameEnd += OnGameEnd;
    }

    void OnGameEnd(bool win)
    {
        _mRenderer = _mTableMaterial.GetComponent<Renderer>();
        _mRenderer.material.SetVector("_Direction", new Vector2(0, 0));
        //_mRenderer.material.SetFloat("_TimeSpeed", 0);
        EndMiniGame(win, miniGameScore);
    }

    public override void Update()
    {
        if(_mTimer.TimerValue <= GameManager.instance.Speed/2.5)
            _mInteractableManager.EndGame();

        if (_mTimer.TimerValue == 0 && _gameIsPlaying)
            EndMiniGame(false, 0);


    }

    private void OnDisable()
    {
        _mInteractableManager.GameEnd -= OnGameEnd;
    }

}
