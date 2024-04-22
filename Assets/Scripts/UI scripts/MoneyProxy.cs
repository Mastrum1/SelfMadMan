using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyProxy : MonoBehaviour
{
    public void AddMoney(int MoneyToAdd)
    {
        MoneyManager.Instance.AddMoney(MoneyToAdd);
    }

    public void Subs(int MoneyToRemove)
    {
        MoneyManager.Instance.SubtractMoney(MoneyToRemove);
    }

    public void SubsEra(TMP_Text price)
    {
        MoneyManager.Instance.SubsEra(price);
    }

    public void SubsSpin(TMP_Text price)
    {
        MoneyManager.Instance.SubsSpin(price);
    }

}
