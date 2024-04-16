using UnityEngine;
using System;
using JetBrains.Annotations;

public class OnCollide : MonoBehaviour
{
    public event Action<bool> OnCollided;
    [SerializeField] private bool _win;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        
        if (gameObject.CompareTag("DirtyAd"))
        {
            gameObject.GetComponent<VFXScaleUp>().OnObjectClicked();
        }
        
        OnCollided?.Invoke(_win);
        
        if (_win)
        {
            col.gameObject.SetActive(false);
        }
    }
}
