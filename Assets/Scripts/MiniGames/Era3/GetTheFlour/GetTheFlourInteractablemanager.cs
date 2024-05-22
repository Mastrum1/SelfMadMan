using System;
using System.Collections.Generic;
using UnityEngine;

public class GetTheFlourInteractablemanager : InteractableManager
{
    public event Action<bool> OnLoseGame;
    
    [SerializeField] private List<FlourBag> _flourBags;

    private int _numBagsToSpawn;

    public int Score => _score;
    private int _score;
    private void Start()
    {
        _numBagsToSpawn = 4 + GameManager.instance.FasterLevel;
        if (_numBagsToSpawn >= 10)
        {
            _numBagsToSpawn = 10;
        }
        
        for (var i = 0; i < _numBagsToSpawn; i++)
        {
            _flourBags[i].gameObject.SetActive(true);
            _flourBags[i].OnLose += HandleLose;
            _flourBags[i].OnBagGrabbed += IncrementNumBagsGrabbed;
        }
    }

    private void IncrementNumBagsGrabbed()
    {
        _score += 5;
    }

    private void HandleLose()
    {
        OnLoseGame?.Invoke(false);
    }

    private void OnDestroy()
    {
        for (var i = 0; i < _numBagsToSpawn; i++)
        {
            _flourBags[i].OnLose -= HandleLose;
        }
    }
}
