using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class BrushMovement : MonoBehaviour
{
    [SerializeField] GameObject _mBrush;
    public Action OnDelete;
    bool _mIsStop;
    
    void Start()
    {
        _mBrush.SetActive(false);
        _mIsStop = false;
    }

    public void OnFingerReleased()
    {
        _mBrush.SetActive(false);
    }

    public void OnFingerDown(Vector3 mPos)
    {
        if (_mIsStop)
            return;
        mPos.z = 0;
        _mBrush.SetActive(true);
        _mBrush.transform.position = mPos;
    }

    public void Stop()
    {
        _mIsStop = true;
    }

    void OnTriggerEnter2D(Collider2D mOther)
    {
        if (_mIsStop)
            return;
        if (mOther.gameObject.CompareTag("ToClean")) {
            mOther.gameObject.SetActive(false);
            OnDelete?.Invoke();
        }
    }
}
