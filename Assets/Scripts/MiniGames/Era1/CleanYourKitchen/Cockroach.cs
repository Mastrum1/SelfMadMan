using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cockroach : MonoBehaviour
{
    public event Action OnTouched;
    
    [SerializeField] private Rigidbody2D mRigid2d;
    private float _speed;
    void Start()
    {
        Move();
    }
    
    void Update()
    {
        transform.up = mRigid2d.velocity;
        CheckSpeed();
    }

    void Move()
    {
        _speed = Random.Range(51f, 60f);
        mRigid2d.AddForce(transform.up * _speed, ForceMode2D.Force);
    }
    void CheckSpeed()
    {
        if (Mathf.Abs(mRigid2d.velocity.x) <= 0.1f)
        {
            mRigid2d.AddForce(new Vector2(_speed, 0), ForceMode2D.Force);
        }
        if (Mathf.Abs(mRigid2d.velocity.y) <= 0.1f)
        {
            mRigid2d.AddForce(new Vector2(0, _speed), ForceMode2D.Force);
        }
    }
    
    public void Touched()
    {
        OnTouched?.Invoke();
        _speed = 0;
        mRigid2d.velocity = new Vector2(0, 0);
        StartCoroutine(DisableCockroach());
    }

    IEnumerator DisableCockroach()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
