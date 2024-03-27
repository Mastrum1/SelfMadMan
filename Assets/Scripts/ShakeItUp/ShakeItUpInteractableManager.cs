using System;
using System.Collections.Generic;
using UnityEngine;

public class ShakeItUpInteractableManager : InteractableManager
{
    public event Action<bool> OnGameEnd;
    
    [SerializeField] private GameObject _proteinParent;

    [SerializeField] private List<Protein> _proteins;
    private int _numProteinDead;
    void Start()
    {
        for (int i = 0; i < _proteinParent.transform.childCount; i++)
        {
            var proteinChild = _proteinParent.transform.GetChild(i).GetComponent<Protein>();
            //_proteins.Add(proteinChild);
            if (proteinChild is null) return;
            
            proteinChild.OnDeath += IncreaseNumDead;
        }
        
        SpawnProteins();
    }
    
    void SpawnProteins()
    {
        foreach (var protein in _proteins)
        {
            var randScale = UnityEngine.Random.Range(0.15f, 0.4f);
            protein.transform.localScale = new Vector3(randScale, randScale, 1);
            protein.enabled = true;
        }
    }

    public void ApplyForce(Vector3 force)
    {
        if (_proteins == null) return;
        foreach (var protein in _proteins)
        {
            protein.ReduceScale(force);
        }
    }

    void IncreaseNumDead()
    {
        _numProteinDead++;
    }

    private void Update()
    {
        if (_numProteinDead >= 4)
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
