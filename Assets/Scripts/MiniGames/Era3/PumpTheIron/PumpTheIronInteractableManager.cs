using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PumpTheIronInteractableManager : InteractableManager
{
    public event Action<bool> OnGameEnd;

    [SerializeField] private GameObject _mInteractablesParent;

    [SerializeField] private SwipeDir _mSwipe;

    [SerializeField] private List<Arrows> _mChilds;

    [SerializeField] private List<ExitArea> _mChildsLeaveArea;



    // Start is called before the first frame update
    void Start()
    {
        _mSwipe.OnSwipeTooEarly += HandleEndGame;

        for (int i = 0; i < _mChilds.Count; i++)
        {
            var addChild = _mChilds[i];
            if (addChild != null)
            {
                _mChildsLeaveArea[i].OnLoose += HandleEndGame;
                addChild.OnAction += HandleSwipe;
            }
        }
    }

    void HandleEndGame(bool win)
    {
        DespawnObjects();
        _mSwipe.EndGame = true;
        OnGameEnd?.Invoke(win);
    }

    public void DisableAllSwipe()
    {
        _mSwipe.EndGame = true;
    }

    public void DespawnObjects()
    {
        for (int i = 0; i < _mChilds.Count; i++)
        {
            var addChild = _mChilds[i];
            if (addChild != null)
            {
                _mChildsLeaveArea[i].EndGame = true;
                addChild.gameObject.SetActive(false);
            }
        }
    }

    void HandleSwipe(bool swipe)
    {
        if (!swipe)
        {
            HandleEndGame(false);
            DespawnObjects();
        }
    }
    private void OnDestroy()
    {
        _mSwipe.OnSwipeTooEarly -= HandleEndGame;
        for (int i = 0; i < _mChilds.Count; i++)
        {
            var addChild = _mChilds[i];
            if (addChild != null)
            {
                _mChildsLeaveArea[i].OnLoose -= HandleEndGame;
                addChild.OnAction -= HandleSwipe;
            }
        }

    }

}
