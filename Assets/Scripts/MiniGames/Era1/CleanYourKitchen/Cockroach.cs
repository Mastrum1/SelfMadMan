using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;
using Random = UnityEngine.Random;

public class Cockroach : MonoBehaviour
{
    public event Action OnTouched;
    
    [SerializeField] private Rigidbody2D _rigid2d;
    [SerializeField] private GameObject _body;
    [SerializeField] private GameObject _anim;
    [SerializeField] private GameObject _squish;
    private float _speed;
    void Start()
    {
        Move();
    }
    
    void Update()
    {
        transform.up = _rigid2d.velocity;
        CheckSpeed();
    }

    void Move()
    {
        _speed = Random.Range(51f, 60f);
        _rigid2d.AddForce(transform.up * _speed, ForceMode2D.Force);
    }
    void CheckSpeed()
    {
        if (Mathf.Abs(_rigid2d.velocity.x) <= 0.1f)
        {
            _rigid2d.AddForce(new Vector2(_speed, 0), ForceMode2D.Force);
        }
        if (Mathf.Abs(_rigid2d.velocity.y) <= 0.1f)
        {
            _rigid2d.AddForce(new Vector2(0, _speed), ForceMode2D.Force);
        }
    }
    
    public void Touched()
    {
        OnTouched?.Invoke();
        _speed = 0;
        _rigid2d.velocity = new Vector2(0, 0);
        StartCoroutine(DisableCockroach());
    }

    IEnumerator DisableCockroach()
    {
        _body.gameObject.SetActive(false);
        _anim.gameObject.SetActive(false);
        _squish.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
