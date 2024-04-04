using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class DirtyAdd : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigid2d;
    private float _forceX;
    private float _forceY;
    private float _dirX;
    private float _dirY;
    

    private void OnEnable()
    {
        _forceX = Random.Range(60, 80);
        _forceY = Random.Range(60, 80);
        _dirX = Random.Range(-1, 1);
        if (_dirX == 0) _dirX = 1;
        _dirY = Random.Range(-1, 1);
        if (_dirY == 0) _dirY = 1;
        
        Move();
    }

    private void Update()
    {
        if (Mathf.Abs(_rigid2d.velocity.x) <= 0.1)
        {
            _rigid2d.AddForce(new Vector2(_forceX, 0));
        }
        if (Mathf.Abs(_rigid2d.velocity.x) <= 0.1)
        {
            _rigid2d.AddForce(new Vector2(_forceY, 0));
        }
    }

    private void Move()
    {
        _rigid2d.AddForce(new Vector2(_forceX * _dirY, _forceY * _dirY));
    }
}
