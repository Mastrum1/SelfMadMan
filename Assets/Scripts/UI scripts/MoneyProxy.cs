using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyProxy : MonoBehaviour
{
    [SerializeField] private Notif _notif;

    public void AddMoney(int MoneyToAdd)
    {
        MoneyManager.Instance.AddMoney(MoneyToAdd);
        _notif.ONOFF();
    }

    public void Subs(int MoneyToRemove)
    {
        MoneyManager.Instance.SubtractMoney(MoneyToRemove);
        _notif.ONOFF();
    }

    public void SubsEra(TMP_Text price)
    {
        MoneyManager.Instance.SubsEra(price);
        _notif.ONOFF();
    }

    public void SubsSpin(TMP_Text price)
    {
        MoneyManager.Instance.SubsSpin(price);
        _notif.ONOFF();
    }
}
