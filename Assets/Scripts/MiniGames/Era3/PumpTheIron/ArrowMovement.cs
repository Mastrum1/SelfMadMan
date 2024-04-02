using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    [SerializeField] int _mSpeed;

    void Update()
    {
        transform.Translate(Vector3.down * _mSpeed * Time.deltaTime);
    }
}
