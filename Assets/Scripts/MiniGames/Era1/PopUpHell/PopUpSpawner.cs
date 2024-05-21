using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PopUpSpawner : MonoBehaviour
{
    public event Action OnClosePopUp;
    
    [SerializeField] private List<GameObject> _mPopUps;
    [SerializeField] private List<GameObject> _mSpawnPoints;
    [SerializeField] private GameObject _mParent;
    [SerializeField] private int _mNumberToSpawn;
    [SerializeField] private GameObject _mButtonToHide;
    //[SerializeField] private GameObject _mDownloadPopUp;

    private List<GameObject> _mPopUpList;

    void Start()
    {
        _mPopUpList = new List<GameObject> ();
        SpawnPopUp(_mPopUps[Random.Range(0, _mPopUps.Count)], _mButtonToHide.transform.position, 6);
        for (int i = 0; i < _mNumberToSpawn - 1; i++) {
            int j = Random.Range(0, _mPopUps.Count);
                SpawnPopUp(_mPopUps[j], RandomPointInBox(), (i * 3) + 20);
        }
        //_mPopUpList.Add(_mDownloadPopUp);
    }

    void SpawnPopUp(GameObject model, Vector3  position, int order)
    {
        GameObject mTmp = Instantiate(model);
        mTmp.GetComponent<SpriteRenderer>().sortingOrder = order;
        mTmp.transform.position = position;
        mTmp.transform.SetParent(_mParent.transform, true);
        mTmp.GetComponent<ADS>().OnCloseAd += AdClosed;
        _mPopUpList.Add(mTmp);
    }

    private void AdClosed()
    {
        OnClosePopUp?.Invoke();
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

    public void DisablePopUp()
    {
        for (int i = 0; i < _mPopUpList.Count; i++)
            _mPopUpList[i].GetComponent<ADS>().DisablePopUp();
    }
}
