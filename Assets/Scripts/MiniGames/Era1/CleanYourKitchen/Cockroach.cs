using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;
using Random = UnityEngine.Random;

public class Cockroach : MonoBehaviour
{
    public event Action OnTouched;
    private AudioManager _audioManager;

    [SerializeField] private Rigidbody2D _rigid2d;
    [SerializeField] private CapsuleCollider2D _collider2D;
    [SerializeField] private GameObject _body;
    [SerializeField] private List<SpriteRenderer> _spriteRenderers;
    public GameObject Body => _body;
    [SerializeField] private GameObject _squish;
    public GameObject Squish => _squish;
    private float _speed;

    private void Start()
    {
        _audioManager = AudioManager.Instance;
        _speed = Random.Range(30f, 50f);
        Move();
    }

    private void Update()
    {
        transform.up = _rigid2d.velocity;
        CheckSpeed();
    }

    private void Move()
    {
        _rigid2d.AddForce(transform.up * _speed, ForceMode2D.Force);
    }

    public void SetLayer(int i)
    {
        foreach (var sprite in _spriteRenderers)
        {
            Debug.Log("haha");
            sprite.sortingOrder = i;
        }
    }
    private void CheckSpeed()
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
        _audioManager.PlaySFX(0);
        OnTouched?.Invoke();
        _speed = 0;
        _rigid2d.velocity = new Vector2(0, 0);
        StartCoroutine(DisableCockroach());
    }

    private IEnumerator DisableCockroach()
    {
        _body.gameObject.SetActive(false);
        _collider2D.enabled = false;
        _squish.gameObject.SetActive(true);
        StartCoroutine(FadeSquish());
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    private IEnumerator FadeSquish()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            _squish.GetComponent<SpriteRenderer>().color -= new Color(0,0,0,4 * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Move();
    }

    private void OnDisable()
    {
        StopCoroutine(DisableCockroach());
        StopCoroutine(FadeSquish());
    }
}
