using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblesPool : MonoBehaviour
{
    public static BubblesPool BubblesSharedInstance;
    private List<GameObject> _mPooledObjects;
    [SerializeField] private GameObject _mBubbles;
    [SerializeField] private GameObject _mParent;
    [SerializeField] private int m_AmountToPool;
    private bool m_IsStopped = false;

    void Awake()
    {
        BubblesSharedInstance = this;
    }

    void Start()
    {
        GameObject mTmp;
        _mPooledObjects = new List<GameObject>();
        for (int i = 0, n = 0; i < m_AmountToPool; i++, n++) {
            mTmp = Instantiate(_mBubbles);
            mTmp.SetActive(false);
            mTmp.transform.SetParent(_mParent.transform, false);
            _mPooledObjects.Add(mTmp);
        }
    }

    public GameObject GetBubbles()
    {
        for(int i = 0; i < _mPooledObjects.Count; i++)
            if(!_mPooledObjects[i].activeInHierarchy)
                return _mPooledObjects[i];
        return NewBubble();
    }

    GameObject  NewBubble()
    {
        GameObject mTmp;
        mTmp = Instantiate(_mBubbles);
        mTmp.SetActive(false);
        mTmp.transform.SetParent(_mParent.transform, false);
        _mPooledObjects.Add(mTmp);
        return mTmp;
    }
}
