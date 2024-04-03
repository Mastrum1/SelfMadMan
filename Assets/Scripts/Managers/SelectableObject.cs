using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectableObject : MonoBehaviour
{

    [SerializeField] private UnityEvent _mOnSelected;

    public void GetSelected()
    {
        Debug.Log("test");
        _mOnSelected?.Invoke();
    }
}
