using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CockroachSpawner : MonoBehaviour
{
    public event Action<int, CockroachSpawner> OnSpawnMore;
    public event Action<Cockroach> OnActivated; 

    [SerializeField] private List<Cockroach> _cockroaches;
    public List<Cockroach> Cockroaches => _cockroaches;
    [SerializeField] private float _percentage;
    public float Percentage => _percentage;

    private int _numToSpawn;
    public int NumToSpawn { get => _numToSpawn; set => _numToSpawn = value; }
    
    private int _numActivated;
    public IEnumerator EnableCockroaches(int amount)
    {
        CheckActive(amount);

        yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
        
        ActivateRoach(amount);
    }

    private void CheckActive(int amount)
    {
        var numActive = _cockroaches.Count(cockroach => cockroach.gameObject.activeSelf);

        if (numActive == _cockroaches.Count && _numActivated < amount)
        {
            OnSpawnMore?.Invoke(1, this);
        }
    }

    private void ActivateRoach(int amount)
    {
        for (var i = 0; i < amount; i++)
        {
            _cockroaches[i].gameObject.SetActive(true);
            _cockroaches[i].transform.position = transform.position;
            _cockroaches[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
            if (_cockroaches[i].gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                _cockroaches[i].gameObject.layer = gameObject.layer;
                _cockroaches[i].transform.localScale = transform.localScale;
            }
            _numActivated++;
            OnActivated?.Invoke(_cockroaches[i]);
            
        }
    }
}
