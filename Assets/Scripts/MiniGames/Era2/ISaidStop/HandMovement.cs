using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class HandMovement : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private float _mSpeed = 2.5f;
    private bool _mIsGet = false;
    private bool _mIsStop = false;
    private Vector3 _mMoveDir;
    private Vector3 _mBackPosition;
    public  event Action CigarPackCaught;

    void Start()
    {
        _mBackPosition = Vector3.zero;
        _mMoveDir = (Target.position - transform.position).normalized;
    }


    void Update()
    {
        if (_mIsGet || _mIsStop)
            return ;
        if (_mBackPosition == Vector3.zero)
            transform.position += _mMoveDir * _mSpeed * Time.deltaTime;
        else {
            float mStep =  _mSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _mBackPosition, mStep);
            _mBackPosition =  (Vector3.Distance(transform.position, _mBackPosition) < 0.1f) ? Vector3.zero : _mBackPosition;
        }
        
    }

    public void Stop()
    {
        _mIsStop = true;
        _mSpeed = 0;
    }

    public void OnSwipe(Vector3 from, Vector3 to)
    {
        Vector3 delta = to - from, gap =  delta.normalized / 1.9f;
        if (delta.x <= 0 && delta.y >= 0)
            _mBackPosition = transform.position - (_mMoveDir * gap.magnitude);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("GameInteractable")) {
            _mIsGet = true;
            CigarPackCaught?.Invoke();
        }
    }
}
