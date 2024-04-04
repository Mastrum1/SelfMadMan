using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterScreen : MonoBehaviour
{
    public delegate void TriggerEnter(Collider2D collide);
    public TriggerEnter triggerEnter;

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Bus"))
            triggerEnter(collider2D);
    }
}
