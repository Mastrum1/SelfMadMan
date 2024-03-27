using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeItUpInteractableManager : InteractableManager
{
    public event Action<bool> OnGameEnd;
    
    [SerializeField] private Protein _proteinParent;

    private int numProteinDead;
    void Start()
    {
        for (int i = 0; i < _proteinParent.transform.childCount; i++)
        {
            var proteinChild = _proteinParent.transform.GetChild(i).GetComponent<Protein>();
            if (proteinChild is null) return;
            
            proteinChild.OnDeath += IncreaseNumDead;
        }
    }

    void IncreaseNumDead()
    {
        numProteinDead++;
    }

    private void Update()
    {
        if (numProteinDead >= 4)
        {
            HandleEndGame();
        }
    }

    void HandleEndGame()
    {
        OnGameEnd?.Invoke(true);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _proteinParent.transform.childCount; i++)
        {
            var proteinChild = _proteinParent.transform.GetChild(i).GetComponent<Protein>();
            if (proteinChild is null) return;
            
            proteinChild.OnDeath -= IncreaseNumDead;
        }
    }
}
