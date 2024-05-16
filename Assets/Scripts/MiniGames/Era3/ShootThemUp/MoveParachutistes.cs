using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveParachutistes : MonoBehaviour
{
    [SerializeField] private Transform _mCar;
    [SerializeField] private float _mSpeed;
    [SerializeField] private ParticleSystem _mParticleSystem;
    private bool _mGotHit = false;

    public event Action<bool> OnLoose;
    void Update()
    {
        if (!_mGotHit)
        {
            transform.Translate(Vector3.down * _mSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "car")
        {
            //gameObject.GetComponent<Animator>().SetBool("EndGame", true);
            OnLoose?.Invoke(false);
            Debug.Log("endGame");
        }
    }

    public void DisableObject()
    {
        _mGotHit = false;
        gameObject.GetComponent<Animator>().SetBool("GetHit", false);
        gameObject.SetActive(false);
        _mParticleSystem.gameObject.SetActive(false);

    }

    public void EcoloGetHit()
    {
        _mGotHit = true;
        _mParticleSystem.gameObject.SetActive(true);
        gameObject.GetComponent<Animator>().SetBool("GetHit", true);
    }
}
