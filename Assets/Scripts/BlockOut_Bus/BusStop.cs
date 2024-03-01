using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusStop : MonoBehaviour
{
    public delegate void TriggerEnter(Collider2D collide);
    public TriggerEnter triggerEnter;
    public delegate void TriggerExit(Collider2D collide);
    public TriggerExit triggerExit;

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Bus"))
            triggerEnter(collider2D);
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Bus"))
            triggerExit(collider2D);
    }
}
