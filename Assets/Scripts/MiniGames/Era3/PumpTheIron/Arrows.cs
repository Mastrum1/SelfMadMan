using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrows : MonoBehaviour
{
    public event Action<bool> OnAction;

    [SerializeField] private string _mDir;
    [SerializeField] private BoxCollider2D _mEndZone;

    private void Start()
    {
        GetComponentInParent<SwipeDir>().OnSwipe += OnSwipe;
    }

    private void OnDisable()
    {
        GetComponentInParent<SwipeDir>().OnSwipe -= OnSwipe;

    }

    public void OnSwipe(string Dir)
    {
        if (Dir == _mDir)
        {
            if (transform.position.y > _mEndZone.bounds.min.y && transform.position.y < _mEndZone.bounds.max.y)
            {
                gameObject.SetActive(false);
                OnAction?.Invoke(true);
            }
            else
            {
                Debug.Log("loose");
                OnAction?.Invoke(false);
            }
        }
    }
}
