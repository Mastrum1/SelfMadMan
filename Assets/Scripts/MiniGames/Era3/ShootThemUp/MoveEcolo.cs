using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveEcolo : MonoBehaviour
{
    [SerializeField] private Transform _mCar;
    [SerializeField] private float _mSpeed;
    [SerializeField] private ParticleSystem _mParticleSystem;
    [SerializeField] private VFXScaleUp _vfxScaleUp;
    [SerializeField] private Animator _animator;
    private bool _mGotHit = false;

    public event Action<bool> OnLoose;

    // Update is called once per frame
    void Update()
    {
        if (!_mGotHit)
        {
            var step = _mSpeed * Time.deltaTime; // calculate distance to move
            transform.position = new Vector3(Vector3.MoveTowards(transform.position, _mCar.position, step).x, transform.position.y, transform.position.z);

        }
    }

    public void StopAllEcolo()
    {
        _animator.SetBool("EndGame", true);
        _animator.speed = 0;
        _mGotHit = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "car")
        {
            _vfxScaleUp.OnObjectClicked();
            _animator.SetBool("EndGame", true);
            OnLoose?.Invoke(false);
        }
    }

    public void DisableObject()
    {
        _mGotHit = false;
        _animator.SetBool("EndGame", false);
        gameObject.SetActive(false);
        _mParticleSystem.gameObject.SetActive(false);

    }

    public void EcoloGetHit()
    {
        _mGotHit = true;
        _mParticleSystem.gameObject.SetActive(true);
        _animator.SetBool("GetHit", true);
    }
}
