using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FindYourChadObj : MonoBehaviour
{
    [SerializeField] private VFXScaleUp _vfxScaleUp;

    [SerializeField] private bool _mIsReal = false;
    public bool Real
    {
        get { return _mIsReal; }
        set { _mIsReal = value; }
    }

    [SerializeField] private SpriteRenderer _SpriteRenderer;
    public SpriteRenderer SpriteRenderer
    {
        get { return _SpriteRenderer; }
        set { _SpriteRenderer = value; }
    }

    [SerializeField] private BoxCollider2D _collider;
   public BoxCollider2D Collider
    {
        get { return _collider; }
        set { _collider = value; }
    }

    public event Action<bool> OnGameEnd;

    public void OnTap()
    {
        if (_mIsReal)
        {
            OnGameEnd?.Invoke(true);
        }
        else
        {
            OnGameEnd?.Invoke(false);
            _vfxScaleUp.OnObjectClicked();
        }
    }
}
