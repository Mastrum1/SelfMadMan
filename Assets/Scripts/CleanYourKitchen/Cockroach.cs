using System;
using System.Collections;
using UnityEngine;

public class Cockroach : MonoBehaviour
{
    public event Action OnTouched;
    
    [SerializeField] private Rigidbody2D mRigid2d;
    [SerializeField] private float _speed;
    void Start()
    {
        Move();
    }
    
    void Update()
    {
        transform.up = mRigid2d.velocity;
    }

    void Move()
    {
        mRigid2d.AddForce(transform.up * _speed, ForceMode2D.Force);
    }

    void Touched()
    {
        OnTouched?.Invoke();
        StartCoroutine(DisableCockroach());
    }

    IEnumerator DisableCockroach()
    {
        yield return new WaitForSeconds(0.5f);
        enabled = false;
    }
}
