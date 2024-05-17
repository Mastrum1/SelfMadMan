using System;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public event Action OnCueBall;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("8Ball")) return;
        //if (other.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude >= 2) return;
        
        OnCueBall?.Invoke();
    }
}
