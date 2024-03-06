using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BusPool : MonoBehaviour
{
    public static BusPool SharedInstance;
    [SerializeField] private List<GameObject> pooledObjects;
    [SerializeField] private GameObject objectToPool;
    [SerializeField] private int amountToPool;
    // Start is called before the first frame update

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        GameObject tmp;
        pooledObjects = new List<GameObject>();
        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public GameObject GetPooledBus()
    {
        for(int i = 0; i < amountToPool; i++)
            if(!pooledObjects[i].activeInHierarchy)
                return pooledObjects[i];
        return null;
    }

    public List<GameObject> GetActiveBus()
    {
        List<GameObject> buses = new List<GameObject>();
        for(int i = 0; i < amountToPool; i++)
            if(pooledObjects[i].activeInHierarchy)
                buses.Add(pooledObjects[i]);
        return buses;
    }
}
