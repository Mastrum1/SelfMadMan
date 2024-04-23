using System;
using UnityEngine;

public class XpBar : MonoBehaviour
{
    public event Action OnLevelUp;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("XpBar")) return;
        
        GameManager.instance.Player.LevelUp();
        OnLevelUp?.Invoke();
    }
}