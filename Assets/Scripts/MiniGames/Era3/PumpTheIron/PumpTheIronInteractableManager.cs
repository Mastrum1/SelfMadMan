using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PumpTheIronInteractableManager : InteractableManager
{
    public event Action<bool> OnGameEnd;

    [SerializeField] private GameObject _mInteractablesParent;


    // Start is called before the first frame update
    void Start()
    {
        _mInteractablesParent.GetComponent<SwipeDir>().OnSwipeTooEarly += HandleEndGame;

        for (int i = 0; i < _mInteractablesParent.transform.childCount; i++)
        {
            var addChild = _mInteractablesParent.transform.GetChild(i).GetComponent<Arrows>();
            if (addChild != null)
            {
                addChild.OnAction += HandleSwipe;
            }
        }
    }

    void HandleEndGame(bool win)
    {
        OnGameEnd?.Invoke(win);
    }

    void HandleSwipe(bool swipe)
    {
        if (!swipe)
        {
            HandleEndGame(false);
        }
    }
    private void OnDestroy()
    {
        _mInteractablesParent.GetComponent<SwipeDir>().OnSwipeTooEarly -= HandleEndGame;
        for (int i = 0; i < _mInteractablesParent.transform.childCount; i++)
        {
            var addChild = _mInteractablesParent.transform.GetChild(i).GetComponent<Arrows>();
            if (addChild != null)
            {
                addChild.OnAction -= HandleSwipe;
            }
        }
    }

}
