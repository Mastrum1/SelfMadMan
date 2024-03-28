using System;
using UnityEditor.Localization.Platform.Android;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    // Private
    GameManager _mGameManager;
    Player _mPlayer;
    public int CurrentMoney { get => _mCurrentMoney; set => _mCurrentMoney = value; }
    [SerializeField] private int _mCurrentMoney;

    void Start()
    {
        _mGameManager = GameManager.instance;
        _mPlayer = _mGameManager.GetComponent<Player>();
        LoadMoney(); // Load PlayerPrefs
    }

    public void AddMoney(int MoneyToAdd)
    {
        _mCurrentMoney += MoneyToAdd;
        _mPlayer.NewCurrency(_mCurrentMoney);
    }

    public void SubtractMoney(int MoneyToRemove)
    {
        if (_mCurrentMoney - MoneyToRemove < 0)
        {
            Debug.Log("No Money");
        }

        else
        {
            _mCurrentMoney -= MoneyToRemove;
            _mPlayer.NewCurrency(_mCurrentMoney);
        }
    }

    private void LoadMoney()
    {
        _mCurrentMoney = _mPlayer.Money;
        Debug.Log("Money loaded: " + _mPlayer.Money);

    }
}