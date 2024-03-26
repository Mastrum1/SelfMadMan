using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class OnAddsCollide : MonoBehaviour
{
    public event Action<bool> OnCollided;
    [SerializeField] public bool Win;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            OnCollided?.Invoke(Win);
        }
    }
}