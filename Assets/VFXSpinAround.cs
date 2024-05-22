using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXSpinAround : MonoBehaviour
{
    public float spinSpeed = 1f; 

    void Update()
    {
        float angle = Time.time * spinSpeed;
        
        transform.rotation = Quaternion.Euler(0, 0, -angle * Mathf.Rad2Deg);
    }
}
