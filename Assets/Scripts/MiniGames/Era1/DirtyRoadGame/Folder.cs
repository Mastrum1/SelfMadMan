using System;
using System.Collections;
using UnityEngine;

public class Folder : MonoBehaviour
{
    private bool _wasTouched;

    private void Awake()
    {
        _wasTouched = false;
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
    }
}
