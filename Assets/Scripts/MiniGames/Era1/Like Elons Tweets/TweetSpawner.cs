using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweetSpawner : MonoBehaviour
{
    public static TweetSpawner SharedInstance;
    private List<GameObject> mPooledObjects;
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
        mPooledObjects = new List<GameObject>();
        for (int i = 0, n = 0; i < m_AmountToPool; i++, n++) {
            if (n == objectsToPool.Count)
                n = 0;
            mTmp = Instantiate(objectsToPool[n]);
            mTmp.SetActive(false);
            mTmp.transform.SetParent(parent.transform, false);
            mPooledObjects.Add(mTmp);
        }
    }

    public GameObject GetPooledTweet()
    {
        if (m_IsStopped) return null;
        List<GameObject> mTweets = GetUnActiveTweet();
        int i = Random.Range(0, mTweets.Count);
        return mTweets[i];
    }

    public List<GameObject> GetUnActiveTweet()
    {
        List<GameObject> mTweets = new List<GameObject>();
        for(int i = 0; i < m_AmountToPool; i++)
            if(!mPooledObjects[i].activeInHierarchy)
                mTweets.Add(mPooledObjects[i]);
        return mTweets;
    }
    public List<GameObject> GetActiveTweets()
    {
        List<GameObject> mTweets = new List<GameObject>();
        for(int i = 0; i < m_AmountToPool; i++)
            if(mPooledObjects[i].activeInHierarchy)
                mTweets.Add(mPooledObjects[i]);
        return mTweets;
    }

    public void StopAllTweetes()
    {
        //isStopped = true;
        for (int i = 0; i < m_AmountToPool; i++)
            if (mPooledObjects[i].activeInHierarchy) {
                mPooledObjects[i].GetComponent<TweetMovement>().Stop();
                mPooledObjects[i].GetComponent<DisplayTweet>().Disable();
            }
    }
}
