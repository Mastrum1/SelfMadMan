using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _mPopUps;
    [SerializeField] private GameObject _mParent;
    [SerializeField] private int _mNumberToSpawn;
    [SerializeField] private Collider2D _mCollider;
    private List<GameObject> _mPopUpList;

    void Start()
    {
        _mPopUpList = new List<GameObject> ();
        for (int i = 0; i < _mNumberToSpawn; i++) {
            int j = Random.Range(0, _mPopUps.Count);
            GameObject mTmp = Instantiate(_mPopUps[j]);
            mTmp.GetComponent<SpriteRenderer>().sortingOrder = (i * 3) + 3;
            mTmp.transform.position = RandomPointInBox();
            mTmp.transform.SetParent(_mParent.transform, true);
            _mPopUpList.Add(mTmp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 RandomPointInBox()
    {
        return _mCollider.bounds.center + new Vector3(
           (Random.value - 0.5f) * _mCollider.bounds.size.x,
           (Random.value - 0.5f) * _mCollider.bounds.size.y,
           0
        );
    }

    public bool IsActivePopUp()
    {
        for (int i = 0; i < _mPopUpList.Count; i++)
            if (_mPopUpList[i].activeInHierarchy)
                return true;
        return false;
    }
}
