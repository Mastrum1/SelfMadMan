using System;
using System.Collections.Generic;
using System.Linq;
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
        DistributeObjects();
    }
    
    private void DistributeObjects()
    {
        var remainingObjects = NumOfCockroach;

        foreach (var cockroachSpawner in _spawnParents)
        {
            cockroachSpawner.NumToSpawn = (int)(NumOfCockroach * (cockroachSpawner.Percentage / 100));
            remainingObjects -= cockroachSpawner.NumToSpawn;
        }

        _spawnParents[0].NumToSpawn += remainingObjects;
        
        foreach (var cockroachSpawner in _spawnParents)
        {
            StartCoroutine(cockroachSpawner.EnableCockroaches(cockroachSpawner.NumToSpawn));
            cockroachSpawner.OnSpawnMore += SpawnCockroaches;
            cockroachSpawner.OnActivated += AddToList;
        }
    }
    
    private void SpawnCockroaches(int amount, CockroachSpawner spawner)
    {
        for (var i = 0; i < amount; i++)
        {
            var roach = Instantiate(_cockroach, _cockroachParent.transform, true);
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
        for (var i = 0; i < _cockroachParent.transform.childCount; i++)
        {
            var cockroachChild = _cockroachParent.transform.GetChild(i).GetComponent<Cockroach>();
            if (cockroachChild != null)
            {
                cockroachChild.OnTouched -= HandleRoachDeath;
            }
        }
    }
}