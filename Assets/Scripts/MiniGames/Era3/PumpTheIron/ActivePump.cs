using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePump : MonoBehaviour
{
    public GameObject Pump;
    // Start is called before the first frame update

    public void ActivePumpObj()
    {
        Pump.SetActive(true);
    }
}
