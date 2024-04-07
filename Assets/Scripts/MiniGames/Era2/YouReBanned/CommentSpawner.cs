using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentSpawner : MonoBehaviour
{
    public static CommentSpawner CommentSharedInstance;
    private List<GameObject> _mPooledObjects;
    [SerializeField] private List<GameObject> _objectsToPool;
    [SerializeField] private GameObject _mParent;
    [SerializeField] private int m_AmountToPool;
    private bool m_IsStopped = false;

    void Awake()
    {
        CommentSharedInstance = this;
    }

    void Start()
    {
        GameObject mTmp;
        _mPooledObjects = new List<GameObject>();
        for (int i = 0, n = 0; i < m_AmountToPool; i++, n++) {
            if (n == _objectsToPool.Count)
                n = 0;
            mTmp = Instantiate(_objectsToPool[n]);
            mTmp.SetActive(false);
            mTmp.transform.SetParent(_mParent.transform, false);
            _mPooledObjects.Add(mTmp);
        }
    }

    public GameObject GetPooledComment()
    {
        if (m_IsStopped) return null;
        List<GameObject> mDisplayComment = GetUnActiveComment();
        int i = Random.Range(0, mDisplayComment.Count);
        return mDisplayComment[i];
    }

    public List<GameObject> GetUnActiveComment()
    {
        List<GameObject> mDisplayComment = new List<GameObject>();
        for(int i = 0; i < m_AmountToPool; i++)
            if(!_mPooledObjects[i].activeInHierarchy)
                mDisplayComment.Add(_mPooledObjects[i]);
        return mDisplayComment;
    }
    public List<GameObject> GetActiveComments()
    {
        List<GameObject> mDisplayComment = new List<GameObject>();
        for(int i = 0; i < m_AmountToPool; i++)
            if(_mPooledObjects[i].activeInHierarchy)
                mDisplayComment.Add(_mPooledObjects[i]);
        return mDisplayComment;
    }

    public void AccelarateActiveComments(Vector3 targetPos)
    {
        for(int i = 0; i < m_AmountToPool; i++)
            if(_mPooledObjects[i].activeInHierarchy) {
                _mPooledObjects[i].GetComponent<CommentMovement>().MoveFaster(targetPos);
            }
    }

    public void StopAllComments()
    {
        for (int i = 0; i < m_AmountToPool; i++)
            if (_mPooledObjects[i].activeInHierarchy) {
                _mPooledObjects[i].GetComponent<CommentMovement>().Stop();
                _mPooledObjects[i].GetComponent<DisplayTikTokComment>().Disable();
            }
    }
}
