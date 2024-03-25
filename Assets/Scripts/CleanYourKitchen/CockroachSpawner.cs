using System.Collections;
using UnityEngine;

public class CockroachSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cockroaches;
    public int numOfCockroach;
    
    void Start()
    {
        StartCoroutine(SpawnCockroaches(numOfCockroach));
    }

    IEnumerator SpawnCockroaches(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject roach = Instantiate(cockroaches, transform.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
            roach.transform.SetParent(transform);
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
        }
    }
}
