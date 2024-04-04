using System;
using Unity.VisualScripting;
using UnityEngine;

public class FailBar : MonoBehaviour
{
    public event Action OnFail;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Milk")) return;
        
        OnFail?.Invoke();
    }
}
