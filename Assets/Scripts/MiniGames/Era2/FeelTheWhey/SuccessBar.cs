using System;
using UnityEngine;

public class SuccessBar : MonoBehaviour
{
    public event Action OnParticleSuccess;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Milk")) return;
        
        OnParticleSuccess?.Invoke();
    }
}
