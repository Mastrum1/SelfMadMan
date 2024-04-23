using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    public event Action OnUpdateMoney;
    
    // Private
    public int CurrentMoney { get => _mCurrentMoney; set => _mCurrentMoney = value; }
    [SerializeField] private int _mCurrentMoney;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        LoadMoney(); // Load PlayerPrefs
        UpdateMoney();
    }

    public void UpdateMoney()
    {
        OnUpdateMoney?.Invoke();
    }

    public void AddMoney(int MoneyToAdd)
    {
        _mCurrentMoney += MoneyToAdd;
        GameManager.instance.Player.NewCurrency(_mCurrentMoney);
        _mCurrentMoney = GameManager.instance.Player.Money;
    }

    public bool SubtractMoney(int MoneyToRemove)
    {
        if (_mCurrentMoney - MoneyToRemove < 0)
        {
            Debug.Log("No Money");
            return false;
        }
        
        _mCurrentMoney -= MoneyToRemove;
        GameManager.instance.Player.NewCurrency(_mCurrentMoney);
        _mCurrentMoney = GameManager.instance.Player.Money;
        return true;
    }

    public void SubsEra(TMP_Text price)
    {
        if (_mCurrentMoney < int.Parse(price.text))
        {
            Debug.Log("No Money");
        }

        else
        {
            _mCurrentMoney -= int.Parse(price.text);
            GameManager.instance.Player.NewCurrency(_mCurrentMoney);
            _mCurrentMoney = GameManager.instance.Player.Money;
        }
    }

    public void SubsSpin(TMP_Text price)
    {
        if (_mCurrentMoney < int.Parse(price.text))
        {
            Debug.Log("No Money");
        }

        else
        {
            _mCurrentMoney -= int.Parse(price.text);
            GameManager.instance.Player.NewCurrency(_mCurrentMoney);
            _mCurrentMoney = GameManager.instance.Player.Money;
        }
    }

    private void LoadMoney()
    {
        _mCurrentMoney = GameManager.instance.Player.Money;
        Debug.Log("Money loaded: " + _mCurrentMoney);
    }
}