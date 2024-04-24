using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PumpTheIronInteractableManager : InteractableManager
{
    public event Action<bool> OnGameEnd;

    public event Action<string> ChangePos;

    [SerializeField] private GameObject _mInteractablesParent;

    [SerializeField] private SwipeDir _mSwipe;

    [SerializeField] private List<Arrows> _mChilds;

    [SerializeField] private List<ArrowMovement> _ArrowMove;

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
        if (!win)
        {
            StopAllArrows();
        }
        else
        {
            DespawnObjects();
        }
        _mSwipe.EndGame = true;
        OnGameEnd?.Invoke(win);
    }

    public void DisableAllSwipe()
    {
        _mSwipe.EndGame = true;
    }

    public void StopAllArrows()
    {
        for (int i = 0; i < _ArrowMove.Count; i++)
        {
            _ArrowMove[i].enabled = false;
        }
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

    void HandleSwipe(bool swipe, string Dir)
    {
        if (!swipe)
        {
            HandleEndGame(false);
            DespawnObjects();
        }
        else
        {
            ChangePos?.Invoke(Dir);
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
