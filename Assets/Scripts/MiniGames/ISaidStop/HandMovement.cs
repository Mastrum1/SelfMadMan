using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HandMovement : MonoBehaviour
{
    [SerializeField] private Transform Target;
    private float _mSpeed = 1.5f;
    private bool _mIsGet = false;
    private bool _mIsStop = false;
    private Vector3 _mMoveDir;
    public  event Action CigarPackCaught;

    void Start()
    {
        _mMoveDir = (Target.position - transform.position).normalized;
    }


    void Update()
    {
        if (!_mIsGet && !_mIsStop)
            transform.position += _mMoveDir * _mSpeed * Time.deltaTime;
    }

    public void Stop()
    {
        _mIsStop = true;
    }

    public void OnSwipe(Vector3 from, Vector3 to)
    {
        Vector3 delta = to - from, gap =  delta.normalized / 2;
        if (delta.x <= 0 && delta.y >= 0)
            transform.position -= _mMoveDir * gap.magnitude;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("GameInteractable")) {
            _mIsGet = true;
            CigarPackCaught?.Invoke();
        }
    }

    /*IEnumerator  AccelerateMovement()
    {
        while (!_mIsGet) {
            yield return new WaitForSeconds(_mDelay);
            _mSpeed += 0.5f;
        }
    }*/
}
