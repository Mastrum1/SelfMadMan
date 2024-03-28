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
    
    private int _numActive;
    private int _numActivated;
    public IEnumerator EnableCockroaches(int amount)
    {
        for (var i = 0; i < amount; i++)
        {
            if (_cockroaches.Any(cockroach => cockroach.gameObject.activeSelf))
            {
                _numActive++;
                if(_numActive == _cockroaches.Count)
                    OnSpawnMore?.Invoke(amount-_numActivated, this);
            }
            foreach (var cockroach in _cockroaches.TakeWhile(cockroach => !cockroach.gameObject.activeSelf))
            {
                cockroach.gameObject.SetActive(true);
                cockroach.transform.position = transform.position;
                cockroach.transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
                if (cockroach.gameObject.layer == LayerMask.NameToLayer("Default"))
                {
                    cockroach.gameObject.layer = gameObject.layer;
                    //cockroach.transform.localScale = new Vector3()
                }
                _numActivated++;
                OnActivated?.Invoke(cockroach);
                yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
            }
        }
    }
}
