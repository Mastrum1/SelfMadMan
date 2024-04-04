using System;
using Unity.VisualScripting;
using UnityEngine;

public class FailBar : MonoBehaviour
{
    public event Action OnFail;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (Math.Abs(other.GetComponent<Rigidbody2D>().velocity.y) <= 0.1f && other.CompareTag("Water"))
        {
            //Debug.Log("Lose");
            OnFail?.Invoke();
        }
    }
}
