using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Mask : MonoBehaviour
{
    bool isCol = false;
    public Action<GameObject> Remove;

    void Update()
    {
        if (!isCol)
            Remove?.Invoke(this.gameObject);
    }
    void OnTriggerEnter2D(Collider2D mOther)
    {
        if (mOther.gameObject.CompareTag("Statue")) {
            isCol = true;
        }
    }
}