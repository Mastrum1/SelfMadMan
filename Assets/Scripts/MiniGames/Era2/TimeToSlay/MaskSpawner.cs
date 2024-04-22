using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class MaskSpawner : MonoBehaviour
{
    public Action OnCompleted;
    [SerializeField] SpriteRenderer _mStatue;
    [SerializeField] GameObject _mObjectMask;
    [SerializeField] GameObject _mParent;
    [SerializeField] float _mSpacing = 0.1f;
    [SerializeField] BrushMovement _mBrush;
    [SerializeField] TMP_Text _mPercentageText;
    [SerializeField] Collider2D _mCollider;
    private int _mObjectRemain;
    private int _mObjectTotal;
    private int _mPercent;
    private List<GameObject> _mObjectsToClean;
    void Start()
    {
        _mParent.gameObject.SetActive(true);
        Vector3 spriteSize = _mCollider.bounds.size;  //_mStatue.bounds.size;
        Vector3 startPos =  _mCollider.bounds.min; //_mStatue.bounds.min;

        int columns = Mathf.CeilToInt(spriteSize.x / _mSpacing);
        int rows = Mathf.CeilToInt(spriteSize.y / _mSpacing);
        
        _mObjectsToClean = GameObject.FindGameObjectsWithTag("ToClean").ToList<GameObject>();
        for (int i = 0; i < columns; i++) {
            for (int j = 0; j < rows; j++) {
                Vector3 position = startPos + new Vector3((i * _mSpacing), j * _mSpacing, 0);
                if (IsSuitableForSpawn(position)) {
                    GameObject mObject = Instantiate(_mObjectMask, position, Quaternion.identity);
                    mObject.transform.SetParent(_mParent.transform, true);
                    mObject.SetActive(true);
                    _mObjectsToClean.Add(mObject);
                }
            }
        }
        _mObjectRemain = _mObjectsToClean.Count;
        _mObjectTotal = _mObjectsToClean.Count;
        _mBrush.OnDelete += OnRemove;
    }

    private bool IsSuitableForSpawn(Vector3 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero, _mSpacing);
        return hit.collider != null && !hit.collider.CompareTag("ToClean");
    }

    void Update()
    {
        _mPercent = (_mObjectTotal - _mObjectRemain ) * 100 / _mObjectTotal;
        _mPercentageText.text = _mPercent.ToString() + "%";
        
        if (_mPercent == 100)
            OnCompleted?.Invoke();
    }

    void OnRemove()
    {
        _mObjectRemain--;
    }

    void OnDestroy()
    {
        _mBrush.OnDelete += OnRemove;
    }

    void DeleteMask(GameObject mObject)
    {
        _mObjectTotal--;
        _mObjectRemain--;
        mObject.SetActive(false);
    }
}
