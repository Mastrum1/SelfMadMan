using System;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    // Private
    public int CurrentMoney { get => _mCurrentMoney; set => _mCurrentMoney = value; }
    [SerializeField] private int _mCurrentMoney;

    void Start()
    {
        LoadMoney(); // Load PlayerPrefs
    }

    public void AddMoney(int MoneyToAdd)
    {
        _mCurrentMoney += MoneyToAdd;
        GameManager.instance.GetComponent<Player>().NewCurrency(_mCurrentMoney);
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
            GameManager.instance.GetComponent<Player>().NewCurrency(_mCurrentMoney);
        }
    }

    private void LoadMoney()
    {
        _mCurrentMoney = GameManager.instance.GetComponent<Player>().Money;
        Debug.Log("Money loaded: " + _mCurrentMoney);
    }
}