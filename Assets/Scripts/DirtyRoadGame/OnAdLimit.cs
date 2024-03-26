using System;
using UnityEngine;

public class OnAdLimit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DirtyAd"))
        {
            other.transform.position = other.GetComponent<DirtyAdd>().OrinalPos;
        }
    }
}