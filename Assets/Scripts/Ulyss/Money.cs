using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    // Private
    public int CurrentMoney { get => _mCurrentMoney; set => _mCurrentMoney = value; }
    [SerializeField] private int _mCurrentMoney;

    [SerializeField] TMP_Text m_TextMeshPro;

    [SerializeField] private ContentManager _mContentManager;

    void Start()
    {
        LoadMoney(); // Load PlayerPrefs
        UpdateMoney();
    }

    public void UpdateMoney()
    {
        m_TextMeshPro.text = GameManager.instance.Player.Money.ToString();
    }

    public void AddMoney(int MoneyToAdd)
    {
        _mCurrentMoney += MoneyToAdd;
        GameManager.instance.GetComponent<Player>().NewCurrency(_mCurrentMoney);
        UpdateMoney();
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
            UpdateMoney();
        }
    }

    public void SubsEra(TMP_Text price)
    {
        if (_mCurrentMoney <= int.Parse(price.text))
        {
            Debug.Log("No Money");
        }

        else
        {
            _mCurrentMoney -= int.Parse(price.text);
            GameManager.instance.Player.NewCurrency(_mCurrentMoney);
            _mContentManager.UnlockEra();
            UpdateMoney();
        }
    }

    private void LoadMoney()
    {
        _mCurrentMoney = GameManager.instance.GetComponent<Player>().Money;
        Debug.Log("Money loaded: " + _mCurrentMoney);
        UpdateMoney();
    }
}