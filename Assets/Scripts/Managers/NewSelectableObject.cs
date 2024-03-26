using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NewSelectableObject : MonoBehaviour
{

    [SerializeField] private UnityEvent _mOnSelected;

    public void GetSelected()
    {
        if(_mOnSelected != null) _mOnSelected.Invoke();
    }
}
