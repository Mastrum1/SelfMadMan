using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CommentBorder : MonoBehaviour
{
    public Action<GameObject> OnEnter;
    
    public Action<GameObject> OnExit;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject != this.transform.parent)
            OnEnter?.Invoke(col.gameObject);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        OnExit?.Invoke(col.gameObject);
    }
}
