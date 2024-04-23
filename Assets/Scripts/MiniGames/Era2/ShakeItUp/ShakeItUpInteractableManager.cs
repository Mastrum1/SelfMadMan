using System;
using System.Collections.Generic;
using UnityEngine;

public class ShakeItUpInteractableManager : InteractableManager
{
    public event Action<bool> OnGameEnd;
    
    [SerializeField] private GameObject _proteinParent;
    [SerializeField] private int _numOfProteins;

    private readonly List<Protein> _proteins = new List<Protein>();
    private int _numProteinDead;
    void Start()
    {
        for (int i = 0; i < _proteinParent.transform.childCount; i++)
        {
            var proteinChild = _proteinParent.transform.GetChild(i).GetComponent<Protein>();
            _proteins.Add(proteinChild);
            if (proteinChild is null) break;
            
            proteinChild.OnDeath += IncreaseNumDead;
        }
        
        SpawnProteins();
    }
    
    void SpawnProteins()
    {
        for (var i = 0; i < _numOfProteins; i++)
        {
            if ( i > _proteins.Count) return;
            var randScale = UnityEngine.Random.Range(0.2f, 0.4f);
            _proteins[i].transform.localScale = new Vector3(randScale, randScale, 1);
            _proteins[i].Resistance = 0.1f; // Change with difficulty
            _proteins[i].gameObject.SetActive(true);
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
