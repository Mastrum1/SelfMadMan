using System.Collections;
using System.Collections.Generic;
using Lean.Common;
using Lean.Touch;
using UnityEngine;

public class ADS : MonoBehaviour
{
    [SerializeField] private List<GameObject> _mSpawnPoints;

    [SerializeField] private GameObject _mCloseButtonModel;
    private GameObject _mCloseButton;

    void Start()
    {
        int i = Random.Range(0, _mSpawnPoints.Count);
        _mCloseButton = Instantiate(_mCloseButtonModel);
        _mCloseButton.transform.position = _mSpawnPoints[i].transform.position;
        _mCloseButton.transform.SetParent(this.transform, true);
        int _mOrder = this.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
        _mCloseButton.GetComponent<SpriteRenderer>().sortingOrder = _mOrder + 1;
        _mCloseButton.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = _mOrder + 2;
        _mCloseButton.GetComponent<LeanSelectableByFinger>().OnSelected.AddListener(OnDelete);
    }

    private void OnDelete(LeanSelect arg0)
    {
        this.transform.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

   /* public void OnDelete()
    {
        this.transform.gameObject.SetActive(false);
    }*/
}
