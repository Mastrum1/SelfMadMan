using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectShotInteractableManager : InteractableManager
{
    // Start is called before the first frame update
    [SerializeField] private Tower _tower;
    public event Action<bool> OnGameEnded;

    public void StopTower()
    {
        _tower.Rotating = false;
        if (_tower.CurrentRotation >= -5f && _tower.CurrentRotation <= 1)
            OnGameEnded(true);
        else
            OnGameEnded(false);
    }
}
