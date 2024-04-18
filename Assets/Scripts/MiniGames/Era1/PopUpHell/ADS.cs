using System.Collections;
using System.Collections.Generic;
using Lean.Common;
using Lean.Touch;
using UnityEngine;
using System;

public class ADS : MonoBehaviour
{
    [SerializeField] private List<GameObject> _mSpawnPoints;
    [SerializeField] Animator _animator;

    [SerializeField] private GameObject _mCloseButtonModel;
    [SerializeField] GameObject _mCloseButton;
    [SerializeField] SpriteRenderer  _mButtonRender;
    [SerializeField] bool _mIsEnable = true;

    void Start()
    {
        int i = UnityEngine.Random.Range(0, _mSpawnPoints.Count);
        _mCloseButton.transform.position = _mSpawnPoints[i].transform.position;
        _mCloseButton.transform.SetParent(this.transform, true);
        int _mOrder = this.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        _mCloseButton.GetComponent<SpriteRenderer>().sortingOrder = _mOrder + 1;
        _mButtonRender.sortingOrder = _mOrder + 2;
    }

    public void OnDelete()
    {
        if (!_mIsEnable)
            return;
        _animator.SetTrigger("Depop");
        StartCoroutine(CloseAd());
    }

    public void DisablePopUp()
    {
        _mIsEnable = false;
    }

    IEnumerator CloseAd()
    {
        yield return new WaitForSeconds(0.3f);
        this.transform.gameObject.SetActive(false); 
    }
}
