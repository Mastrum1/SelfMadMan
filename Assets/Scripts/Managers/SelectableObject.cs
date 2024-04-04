using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectableObject : MonoBehaviour
{

    [SerializeField] private UnityEvent _mOnSelected;
    [SerializeField] private UnityEvent _mOnDeselected;

    public void GetSelected()
    {
        _mOnSelected?.Invoke();
    }

    public void GetDeselected()
    {
        Debug.Log("test");
        _mOnDeselected?.Invoke();
    }
}
