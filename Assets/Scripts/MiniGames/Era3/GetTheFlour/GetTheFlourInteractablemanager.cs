using System;
using System.Collections.Generic;
using UnityEngine;

public class GetTheFlourInteractablemanager : InteractableManager
{
    public event Action<bool> OnLoseGame;
    
    [SerializeField] private List<FlourBag> _flourBags;

    private int numBagsToSpawn;
    private void Start()
    {
        numBagsToSpawn = 4 + GameManager.instance.FasterLevel;
        if (numBagsToSpawn >= 10)
        {
            numBagsToSpawn = 10;
        }
        
        for (var i = 0; i < numBagsToSpawn; i++)
        {
            _flourBags[i].gameObject.SetActive(true);
            _flourBags[i].OnLose += HandleLose;
        }
    }

    private void HandleLose()
    {
        OnLoseGame?.Invoke(false);
    }

    private void OnDestroy()
    {
        for (var i = 0; i < numBagsToSpawn; i++)
        {
            _flourBags[i].OnLose -= HandleLose;
        }
    }
}
