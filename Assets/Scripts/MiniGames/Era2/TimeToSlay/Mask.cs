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
        bool mIsHidingChad = false;
        ContactFilter2D filter2D = new ContactFilter2D();
        List<Collider2D> obj =  new List<Collider2D>();
        Physics2D.OverlapCircle(this.transform.position, 0.01f,  filter2D, obj);
        for (int i = 0; i < obj.Count; i++)
            if(obj[i].gameObject.CompareTag("Statue"))
                mIsHidingChad = true;
        if (!mIsHidingChad)
            Remove?.Invoke(this.gameObject);
    }
}