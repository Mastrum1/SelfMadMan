using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _mPopUps;
    [SerializeField] private List<GameObject> _mSpawnPoints;
    [SerializeField] private GameObject _mParent;
    [SerializeField] private int _mNumberToSpawn;
    [SerializeField] private GameObject _mButtonToHide;
    private List<GameObject> _mPopUpList;

    void Start()
    {
        _mPopUpList = new List<GameObject> ();
       // SpawnPopUp(_mPopUps[Random.Range(0, _mPopUps.Count)], _mButtonToHide.transform.position, 6);
       // SpawnPopUp(_mPopUps[Random.Range(0, _mPopUps.Count)], _mButtonToHide.bounds.center, 6);
        for (int i = 0; i < _mNumberToSpawn - 2; i++) {
            int j = Random.Range(0, _mPopUps.Count);
            if (i == 0)
                SpawnPopUp(_mPopUps[j], RandomPointInBox(), (i * 3) + 20);
            else
                SpawnPopUp(_mPopUps[j], RandomPointInBox(), (i * 3) + 20);
            /*GameObject mTmp = Instantiate(_mPopUps[j]);
            mTmp.GetComponent<SpriteRenderer>().sortingOrder = (i * 3) + 3;
            mTmp.transform.position = RandomPointInBox();
            mTmp.transform.SetParent(_mParent.transform, true);
            _mPopUpList.Add(mTmp);*/
        }
    }

    void SpawnPopUp(GameObject model, Vector3  position, int order)
    {
        GameObject mTmp = Instantiate(model);
        mTmp.GetComponent<SpriteRenderer>().sortingOrder = order;
        mTmp.transform.position = position;
        mTmp.transform.SetParent(_mParent.transform, true);
        _mPopUpList.Add(mTmp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 RandomPointInBox()
    {
        return new Vector3(
            Random.Range( _mSpawnPoints[0].transform.position.x, _mSpawnPoints[1].transform.position.x),
            Random.Range(_mSpawnPoints[0].transform.position.y, _mSpawnPoints[2].transform.position.y),
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
