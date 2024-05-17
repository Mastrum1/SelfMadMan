using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRespawn : MonoBehaviour
{
    public event Action<bool> OnGameEnd;
    [SerializeField] private RizzHerInteractableManager _interactableManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ObstacleParent"))
        {
            _interactableManager.SpawnNewObj();
        }
    }
}
