using UnityEngine;
using System;

public class OnCollide : MonoBehaviour
{
    public event Action<bool> OnCollided;
    [SerializeField] private bool _win;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        
        OnCollided?.Invoke(_win);
        if (_win)
        {
            col.gameObject.SetActive(false);
        }
    }
}
