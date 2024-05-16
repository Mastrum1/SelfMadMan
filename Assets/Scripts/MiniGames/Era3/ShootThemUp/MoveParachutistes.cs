using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveParachutistes : MonoBehaviour
{
    [SerializeField] private Transform _mCar;
    [SerializeField] private float _mSpeed;
    [SerializeField] private ParticleSystem _mParticleSystem;
    [SerializeField] private VFXScaleUp _vfxScaleUp;
    [SerializeField] private Animator _animator;

    private bool _mGotHit = false;

    public event Action<bool> OnLoose;
    void Update()
    {
        if (!_mGotHit)
        {
            transform.Translate(Vector3.down * _mSpeed * Time.deltaTime);
        }
    }

    public void StopAllEcolo()
    {
        _mGotHit = true;
        _animator.speed = 0;

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
