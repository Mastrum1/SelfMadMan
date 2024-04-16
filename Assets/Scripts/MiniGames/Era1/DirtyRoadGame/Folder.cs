using System;
using System.Collections;
using UnityEngine;

public class Folder : MonoBehaviour
{
    private Vector3 _startPos;
    private bool _wasTouched;
    private void Awake()
    {
        _wasTouched = false;
        _startPos = transform.position;
    }
    public void Move(Vector3 position)
    {
        if (!_wasTouched)
        {
            transform.position = position;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("DirtyAd")) return;
        _wasTouched = true;
        transform.position = _startPos;
    }
}
