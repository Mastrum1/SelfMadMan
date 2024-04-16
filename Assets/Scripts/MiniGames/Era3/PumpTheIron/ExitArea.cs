using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitArea : MonoBehaviour
{
    public event Action<bool> OnLoose;

    [SerializeField] private Arrows _mParent;

    private bool _mEndGame = false;
    public bool EndGame { get => _mEndGame; set => _mEndGame = value; }

    private void OnEnable()
    {
        _mEndGame = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _mParent.enabled = true;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!_mEndGame)
        {
            _mParent.gameObject.SetActive(false);
            _mParent.enabled = false;
            OnLoose?.Invoke(false);
        }
    }
}
