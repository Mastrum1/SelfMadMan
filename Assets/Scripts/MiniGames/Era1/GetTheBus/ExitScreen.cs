using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScreen : MonoBehaviour
{
    public delegate void TriggerExit(Collider2D collide);
    public TriggerExit triggerExit;

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Bus"))
            triggerExit(collider2D);
    }
}
