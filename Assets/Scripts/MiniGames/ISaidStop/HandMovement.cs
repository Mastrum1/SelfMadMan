using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HandMovement : MonoBehaviour
{
    [SerializeField] private Transform Target;
    private float _mSpeed = 1.5f;
    private bool _mIsGet = false;
    public  event Action CigarPackCaught;

    void Update()
    {
        Vector3 mMoveDir = (Target.position - transform.position).normalized;
        if (!_mIsGet)
            transform.position += mMoveDir * _mSpeed * Time.deltaTime;
    }

    public void OnSwipe(Vector3 from, Vector3 to)
    {
        Vector3 delta = to - from, gap =  delta / 4;

        if (delta.x <= 0 && delta.y >= 0) {
            transform.position += gap;
            Debug.Log("Yessssssssssssss");
        }
        else
            Debug.Log("Nooo");
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
