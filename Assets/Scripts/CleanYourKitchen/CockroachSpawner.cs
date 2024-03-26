using System.Collections;
using UnityEngine;

public class CockroachSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cockroach;
    [SerializeField] private GameObject _cockroachParent;
    [SerializeField] private int _numOfCockroach;
    
    void Start()
    {
        StartCoroutine(SpawnCockroaches(_numOfCockroach));
    }

    IEnumerator SpawnCockroaches(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var roach = Instantiate(_cockroach, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
            roach.transform.SetParent(_cockroachParent.transform);
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
        }
    }
}
