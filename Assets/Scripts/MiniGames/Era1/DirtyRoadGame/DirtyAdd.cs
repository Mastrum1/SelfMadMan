using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class DirtyAdd : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigid2d;
    [SerializeField] private float _force;

    private void Start()
    {
        Move();
    }

    private void Move()
    {
        _force = Random.Range(60, 150);
        _rigid2d.AddForce(transform.up * _force);
    }
}
