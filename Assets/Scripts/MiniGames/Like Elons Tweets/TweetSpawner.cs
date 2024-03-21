using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweetSpawner : MonoBehaviour
{
    public static TweetSpawner SharedInstance;
    private List<GameObject> pooledObjects;
    [SerializeField] private List<GameObject> objectsToPool;
    [SerializeField] private GameObject parent;
    [SerializeField] private int m_AmountToPool;
    private bool m_IsStopped = false;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        GameObject mTmp;
        pooledObjects = new List<GameObject>();
        for (int i = 0, n = 0; i < m_AmountToPool; i++) {
            n = Random.Range(0, objectsToPool.Count);
            mTmp = Instantiate(objectsToPool[n]);
            mTmp.SetActive(false);
            mTmp.transform.SetParent(parent.transform, false);
            pooledObjects.Add(mTmp);
        }
    }

    public GameObject GetPooledTweet()
    {
        if (m_IsStopped) return null;
        for (int i = 0; i < m_AmountToPool; i++)
            if(!pooledObjects[i].activeInHierarchy)
                return pooledObjects[i];
        return null;
    }

    public List<GameObject> GetActiveTweet()
    {
        List<GameObject> Tweets = new List<GameObject>();
        for(int i = 0; i < m_AmountToPool; i++)
            if(pooledObjects[i].activeInHierarchy)
                Tweets.Add(pooledObjects[i]);
        return Tweets;
    }

    public void StopAllTweetes()
    {
        /*isStopped = true;
        for (int i = 0; i < amountToPool; i++)
            if (pooledObjects[i].activeInHierarchy)
                //pooledObjects[i].GetComponent<BusMovement>().Stop();*/
    }
}
