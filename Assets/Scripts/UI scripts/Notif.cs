using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notif : MonoBehaviour
{
    public static Notif Instance;

    [SerializeField] GameObject _mNotif;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        ONOFF();
    }

    public void ONOFF()
    {
        if (MoneyManager.Instance.CurrentMoney >= 100)
        {
            _mNotif.SetActive(true);
        }
        else if (MoneyManager.Instance.CurrentMoney < 100)
        {
            _mNotif.SetActive(false);
        }
    }
}
