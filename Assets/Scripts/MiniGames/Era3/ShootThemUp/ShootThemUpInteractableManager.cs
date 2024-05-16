using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShootThemUpInteractableManager : InteractableManager
{
    public event Action<bool> OnGameEnd;
    [SerializeField] private List<MoveParachutistes> _Parachutistes;
    [SerializeField] private List<MoveEcolo> _Ecolo;

    private void Start()
    {
        for (int i = 0; i < _Ecolo.Count; i++)
        {
            _Ecolo[i].OnLoose += HandleEndGame;
        }
        for (int i = 0; i < _Parachutistes.Count; i++)
        {
            Debug.Log("Parachutistes");
            _Parachutistes[i].OnLoose += HandleEndGame;
        }
    }

    void HandleEndGame(bool win)
    {
        for(int i = 0; i < _Ecolo.Count; i++)
        {
            _Ecolo[i].StopAllEcolo();
        }
        for (int i = 0; i < _Parachutistes.Count; i++)
        {
            _Parachutistes[i].StopAllEcolo();
        }
        OnGameEnd?.Invoke(win);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _Ecolo.Count; i++)
        {
            _Ecolo[i].OnLoose -= HandleEndGame;
        }
        for (int i = 0; i < _Parachutistes.Count; i++)
        {
            _Parachutistes[i].OnLoose -= HandleEndGame;
        }
    }


}
