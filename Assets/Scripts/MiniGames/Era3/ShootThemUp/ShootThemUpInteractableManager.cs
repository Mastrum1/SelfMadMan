using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShootThemUpInteractableManager : InteractableManager
{
    public event Action<bool> OnGameEnd;
    [SerializeField] private List<MoveEcolo> _AllEcolo;

    private void Start()
    {
        for (int i = 0; i < _AllEcolo.Count; i++)
        {
            _AllEcolo[i].OnLoose += HandleEndGame;
        }
    }

    void HandleEndGame(bool win)
    {
        OnGameEnd?.Invoke(win);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _AllEcolo.Count; i++)
        {
            _AllEcolo[i].OnLoose -= HandleEndGame;
        }
    }


}
