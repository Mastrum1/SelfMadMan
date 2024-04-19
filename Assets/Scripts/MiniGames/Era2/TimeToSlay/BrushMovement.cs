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
            if (UnityEngine.Random.value < 0.1f) {
                GameObject _mBubbles = BubblesPool.BubblesSharedInstance.GetBubbles();
                _mBubbles.transform.position = mOther.transform.position;
                _mBubbles.transform.localScale = _mBubbles.transform.localScale * UnityEngine.Random.Range(1, 2);
                _mBubbles.SetActive(true);
                _mBubbles.GetComponent<BubblesLifeTime>().InitBubble();
            }
            mOther.gameObject.SetActive(false);
            OnDelete?.Invoke();
        }
    }
}
