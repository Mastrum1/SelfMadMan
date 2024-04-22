using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShootThemUpInteractableManager : InteractableManager
{
    public event Action<bool> OnGameEnd;
    [SerializeField] private GameObject _mParents;

    private void Start()
    {
        for (int i = 0; i < _mParents.transform.childCount; i++)
        {
            var addChild = _mParents.transform.GetChild(i).GetComponent<MoveEcolo>();
            if (addChild != null)
            {
                addChild.OnLoose += HandleEndGame;
            }
        }
    }

    void HandleEndGame(bool win)
    {
        OnGameEnd?.Invoke(win);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _mParents.transform.childCount; i++)
        {
            var addChild = _mParents.transform.GetChild(i).GetComponent<MoveEcolo>();
            if (addChild != null)
            {
                addChild.OnLoose -= HandleEndGame;
            }
        }
    }


}
