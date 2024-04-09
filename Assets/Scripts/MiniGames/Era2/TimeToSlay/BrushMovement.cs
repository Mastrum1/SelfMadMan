using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BrushMovement : MonoBehaviour
{
    [SerializeField] GameObject _mBrush;
    public Action OnDelete;
    
    void Start()
    {
        _mBrush.SetActive(false);
    }

    public void OnFingerReleased()
    {
        _mBrush.SetActive(false);
    }

    public void OnFingerDown(Vector3 mPos)
    {
        mPos.z = 0;
        _mBrush.SetActive(true);
        _mBrush.transform.position = mPos;
    }

    void OnTriggerEnter2D(Collider2D mOther)
    {
        if (mOther.gameObject.CompareTag("ToClean")) {
            mOther.gameObject.SetActive(false);
            OnDelete?.Invoke();
        }
    }
}
