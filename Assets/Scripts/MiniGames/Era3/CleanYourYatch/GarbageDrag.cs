using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageDrag : MonoBehaviour
{
    //public void OnSeleted
    [SerializeField] float _mScaleFactor = 1.5f;
    bool _mIsSelected = false;
    bool _mIsEnable = true;
    public void OnDrag(Vector3 pos)
    {
        if (!_mIsEnable)
            return;
        transform.position = new Vector3(pos.x, pos.y, 0);
    }

    public void OnSeleted()
    {
        if (!_mIsEnable)
            return;
        if (!_mIsSelected) {
            transform.localScale = transform.localScale * _mScaleFactor;
            _mIsSelected = true;
        }
    }

    public void OnRealesed()
    {
        if (_mIsSelected) {
            transform.localScale = transform.localScale / _mScaleFactor;
            _mIsSelected = false;
        }
    }

    public void Disable()
    {
        _mIsEnable = false;
    }
}