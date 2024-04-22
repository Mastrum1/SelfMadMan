using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserInfos : MonoBehaviour
{
    [SerializeField] private TMP_Text _mMoneyText;
    [SerializeField] private TMP_Text _mPlayerLevel;
    [SerializeField] private TMP_Text _mPlayerName;

    private void OnEnable()
    {
        MoneyManager.Instance.OnUpdateMoney += UpdateMoneyText;
    }

    private void OnDisable()
    {
        MoneyManager.Instance.OnUpdateMoney -= UpdateMoneyText;
    }

    private void UpdateMoneyText()
    {
        _mMoneyText.text = GameManager.instance.Player.Money.ToString();
    }
}
