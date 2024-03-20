using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Transform Target;
    private Rigidbody2D _mRigidBody;
    private float _mSpeed = 1;
    private bool _mIsGet = false;

    public delegate void CigarPackCaught();
    public CigarPackCaught PackCaught;

    private float _mDelay;

    void Start()
    {
        _mDelay = GameManager.instance.Speed / 10;
        _mRigidBody = GetComponent<Rigidbody2D>();
        StartCoroutine(AccelerateMovement());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mMoveDir = (Target.position - transform.position).normalized;
        if (!_mIsGet) {
            transform.position += mMoveDir * _mSpeed * Time.deltaTime;
            PackCaught();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("GameInteractable")) {
            _mIsGet = true;
            PackCaught();
        }
    }

    IEnumerator  AccelerateMovement()
    {
        while (!_mIsGet) {
            yield return new WaitForSeconds(_mDelay);
            _mSpeed++;
        }
    }
}
