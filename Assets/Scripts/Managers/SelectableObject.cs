using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectableObject : MonoBehaviour
{

    [SerializeField] private UnityEvent _mOnSelected;

    public void GetSelected()
    {
        if(_mOnSelected != null) _mOnSelected.Invoke();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
