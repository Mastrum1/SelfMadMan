using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShakeItUpInteractableManager : InteractableManager
{
    public event Action<bool> OnGameEnd;
    
    [SerializeField] private int _numOfProteins;
    [SerializeField] private List<Protein> _proteins;

    public int NumProteinDead => _numProteinDead;
    private int _numProteinDead;

    private void Start()
    {
        _numProteinDead = 0;
        
        EnableProteins();
        
        foreach (var protein in _proteins.Where(protein => protein.gameObject.activeSelf))
        {
            protein.OnDeath += IncreaseNumDead;
        }
    }

    private void EnableProteins()
    {
        for (var i = 0; i < _numOfProteins; i++)
        {
            if ( i > _proteins.Count) return;
            var randScale = UnityEngine.Random.Range(0.2f, 0.4f);
            _proteins[i].transform.localScale = new Vector3(randScale, randScale, 1);
            _proteins[i].Resistance = 0.1f + (float)GameManager.instance.FasterLevel / 100;
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

    private void IncreaseNumDead()
    {
        if (++_numProteinDead >= _numOfProteins)
        {
            OnGameEnd?.Invoke(true);
        }
    }

    private void OnDisable()
    {
        foreach (var protein in _proteins.Where(protein => protein.gameObject.activeSelf))
        {
            protein.OnDeath -= IncreaseNumDead;
        }
    }
}
