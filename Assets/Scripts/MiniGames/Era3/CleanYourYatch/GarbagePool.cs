using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbagePool : MonoBehaviour
{
    public static GarbagePool GarbageSharedInstance;
    [SerializeField] private List<GameObject> m_PooledObjects;
    [SerializeField] private GameObject m_ObjectPool;
    [SerializeField] private int m_AmountToPool;
    [SerializeField] private InputManager _mInputManager;
    private bool isStopped = false;

    void Awake()
    {
        GarbageSharedInstance = this;
    }

    void Start()
    {
        GameObject tmp;
        m_PooledObjects = new List<GameObject>();
        for(int i = 0; i < m_AmountToPool; i++)
        {
            tmp = Instantiate(m_ObjectPool);
            tmp.SetActive(false);
            m_PooledObjects.Add(tmp);
        }
    }

    public GameObject GetPooledGarbage()
    {
        if (isStopped) return null;
        for (int i = 0; i < m_AmountToPool; i++)
            if(!m_PooledObjects[i].activeInHierarchy)
                return m_PooledObjects[i];
        return null;
    }

    public List<GameObject> GetActiveGarbage()
    {
        List<GameObject> mGarbages = new List<GameObject>();
        for(int i = 0; i < m_AmountToPool; i++)
            if(m_PooledObjects[i].activeInHierarchy)
                mGarbages.Add(m_PooledObjects[i]);
        return mGarbages;
    }

    public void HideAllGarbages()
    {
        for (int i = 0; i < m_AmountToPool; i++)
            if (m_PooledObjects[i].activeInHierarchy)
                m_PooledObjects[i].SetActive(false);
    }
}