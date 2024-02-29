using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Folder : MonoBehaviour
{
    private InputManager _mInput;
    // Start is called before the first frame update
    void Start()
    {
        _mInput = InputManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        OnDrag();
    }

    private void OnDrag()
    {
        if (_mInput.isDragging)
        {
            RaycastHit2D hit = Physics2D.Raycast(_mInput.PrimaryPos(), Vector2.zero);
            if (hit.collider == gameObject.GetComponent<Collider2D>())
            {
                transform.position = _mInput.PrimaryPos();
            }
        }
    }
}
