using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notif : MonoBehaviour
{
    public void ONOFF()
    {
        if (MoneyManager.Instance.CurrentMoney >= 100)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
