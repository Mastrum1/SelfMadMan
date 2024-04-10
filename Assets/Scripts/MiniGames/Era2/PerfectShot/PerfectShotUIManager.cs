using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PerfectShotUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _WinParticles;

    public void OnWin()
    {
        _WinParticles.SetActive(true);
    }

}
