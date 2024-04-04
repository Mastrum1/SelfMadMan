using System;
using UnityEngine;

public class SuccessBar : MonoBehaviour
{
    public event Action OnParticleSuccess;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (Math.Abs(other.GetComponent<Rigidbody2D>().velocity.y) <= 0.1f && other.CompareTag("Water"))
        {
            //Debug.Log("win");
            OnParticleSuccess?.Invoke();
        }
    }
}
