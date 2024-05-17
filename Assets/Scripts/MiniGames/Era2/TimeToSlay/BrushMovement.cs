using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Linq;

public class BrushMovement : MonoBehaviour
{
    [SerializeField] GameObject _mBrush;
    public Action OnDelete;
    bool _mIsStop;
    [SerializeField] private RectTransform _PauseButton;
    bool _ScreenPressed = false;

    void Start()
    {
        // _mBrush.SetActive(false);
        _mIsStop = false;
    }

    public void OnFingerReleased()
    {
        _mBrush.SetActive(false);
        _ScreenPressed = false;
    }

    public void OnFingerPressed()
    {
        if (!_mIsStop)
            _ScreenPressed = true;
        //_mBrush.SetActive(true);
    }

    public void OnFingerDown(Vector3 mPos)
    {
        if (_ScreenPressed)
        {
            Vector3 PauseButtonPos = RectTransformUtility.WorldToScreenPoint(Camera.main, _PauseButton.position);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(PauseButtonPos.x, PauseButtonPos.y, 0));

            if (mPos.x > worldPos.x - 0.6f && mPos.x < worldPos.x + 0.6f && mPos.y < worldPos.y + 0.6f && mPos.y > worldPos.y - 0.6f)
                return;
            else
            {
                if (!_mBrush.activeSelf)
                    _mBrush.SetActive(true);

                if (_mIsStop)
                    return;
                mPos.z = 0;
                if (_mBrush.activeInHierarchy)
                {
                    _mBrush.transform.position = mPos;
                    CheckGarbage();
                }
            }
        }
    }


    public void Stop()
    {
        _mIsStop = true;
    }

    void CheckGarbage()
    {
        Collider2D[] obj = Physics2D.OverlapBoxAll(this.transform.position, new Vector2(0.6f, 0.3f), 0);
        for (int i = 0; i < obj.Length; i++)
            if (obj[i].gameObject.CompareTag("ToClean"))
            {
                if (UnityEngine.Random.value < 0.1f)
                {
                    GameObject _mBubbles = BubblesPool.BubblesSharedInstance.GetBubbles();
                    _mBubbles.transform.position = obj[i].transform.position;
                    _mBubbles.transform.localScale = _mBubbles.transform.localScale * UnityEngine.Random.Range(1, 2);
                    _mBubbles.SetActive(true);
                }
                obj[i].gameObject.SetActive(false);
                OnDelete?.Invoke();
            }
    }
}
