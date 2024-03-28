using System;
using System.Collections.Generic;
using UnityEngine;

public class CleanYourKitchenInteractableManager : InteractableManager
{
    public event Action OnRoachDeath;
    
    [SerializeField] private GameObject _cockroach;
    [SerializeField] private GameObject _cockroachParent;
    [SerializeField] private List<CockroachSpawner> _spawnParents;
    private List<Cockroach> _cockroaches = new List<Cockroach>();
    public int NumOfCockroach { get; set; }
    private void Start()
    {
        foreach (var cockroachSpawner in _spawnParents)
        {
            StartCoroutine(cockroachSpawner.EnableCockroaches((int)(NumOfCockroach * (cockroachSpawner.Percentage / 100))));
            cockroachSpawner.OnSpawnMore += SpawnCockroaches;
            cockroachSpawner.OnActivated += AddToList;
        }
    }
    
    private void SpawnCockroaches(int amount, CockroachSpawner spawner)
    {
        for (int i = 0; i < amount; i++)
        {
            var roach = Instantiate(_cockroach);
            roach.transform.SetParent(_cockroachParent.transform);
            spawner.Cockroaches.Add(roach.GetComponent<Cockroach>());
        }
    }

    private void AddToList(Cockroach roach)
    {
        _cockroaches.Add(roach);
        roach.OnTouched += HandleRoachDeath;
    }

    private void HandleRoachDeath()
    {
        OnRoachDeath?.Invoke();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _cockroachParent.transform.childCount; i++)
        {
            var cockroachChild = _cockroachParent.transform.GetChild(i).GetComponent<Cockroach>();
            if (cockroachChild != null)
            {
                cockroachChild.OnTouched -= HandleRoachDeath;
            }
        }
    }
}