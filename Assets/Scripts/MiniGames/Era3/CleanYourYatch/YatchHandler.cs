using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class YatchHandler : MonoBehaviour
{
    public  event Action GarbageDeleted;

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("GameInteractable")) {
            other.gameObject.SetActive(false);
            GarbageDeleted?.Invoke();
        }
    }
}
