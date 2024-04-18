using System.Collections;
using System.Collections.Generic;
using Lean.Common;
using Lean.Touch;
using UnityEngine;

public class ADS : MonoBehaviour
{
    [SerializeField] private List<GameObject> _mSpawnPoints;
    [SerializeField] Animator _animator;

    [SerializeField] private GameObject _mCloseButtonModel;
    [SerializeField] GameObject _mCloseButton;
    [SerializeField] SpriteRenderer  _mButtonRender;

    void Start()
    {
        int i = Random.Range(0, _mSpawnPoints.Count);
       // _mCloseButton = Instantiate(_mCloseButtonModel);
        _mCloseButton.transform.position = _mSpawnPoints[i].transform.position;
        _mCloseButton.transform.SetParent(this.transform, true);
        int _mOrder = this.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        _mCloseButton.GetComponent<SpriteRenderer>().sortingOrder = _mOrder + 1;
        _mButtonRender.sortingOrder = _mOrder + 2;
        //_mCloseButton.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = _mOrder + 2;
  //_mCloseButton.GetComponent<LeanSelectableByFinger>().OnSelected.AddListener(OnDelete);
        //_mCloseButton.GetComponent<SelectableObject>().
    }

    public void OnDelete()
    {
        _animator.SetTrigger("Depop");
        StartCoroutine(CloseAd());
    }

    IEnumerator CloseAd()
    {
        yield return new WaitForSeconds(0.3f);
        this.transform.gameObject.SetActive(false); 
    }
}
