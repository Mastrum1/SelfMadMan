using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetBenchedInteractableManager : MonoBehaviour
{
    public event Action<bool> OnGameEnd;

    [SerializeField] private GameObject _mInteractablesParent;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _mInteractablesParent.transform.childCount; i++)
        {
            var addChild = _mInteractablesParent.transform.GetChild(i).GetComponent<TapWithTimer>();
            if (addChild != null)
            {
                addChild.OnLoose += HandleEndGame;
            }
        }
    }

    void HandleEndGame(bool win)
    {
        for (int i = 0; i < _mInteractablesParent.transform.childCount; i++)
        {
            _mInteractablesParent.transform.GetChild(i).GetComponent<TapWithTimer>().StopTorus = true;
        }
        OnGameEnd?.Invoke(win);
        
    }
    private void OnDestroy()
    {
        for (int i = 0; i < _mInteractablesParent.transform.childCount; i++)
        {
            var addChild = _mInteractablesParent.transform.GetChild(i).GetComponent<TapWithTimer>();
            if (addChild != null)
            {
                addChild.OnLoose -= HandleEndGame;
            }
        }
    }

}
