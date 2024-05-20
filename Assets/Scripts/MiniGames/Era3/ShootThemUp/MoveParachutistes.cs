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

    [SerializeField] private float scaleUpDuration = 0.5f;
    [SerializeField] private float scaleDownDuration = 0.5f;
    [SerializeField] private float scaleUpFactor = 1.3f;
    private bool isScaling = false;

    private bool _mGotHit = false;

    private Vector3 originalScale;

    public event Action<bool> OnLoose;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (isScaling)
        {
            // Scale the object smoothly
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * 2f);
        }
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
            StartCoroutine(ScaleObject());
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

    private IEnumerator ScaleObject()
    {
        while (true)
        {
            Debug.Log("ScaleObject");

            isScaling = true;

            // Scale up to 1.3
            float timer = 0f;
            Vector3 targetScale = originalScale * scaleUpFactor;

            while (timer < scaleUpDuration)
            {
                transform.localScale = Vector3.Lerp(originalScale, targetScale, timer / scaleUpDuration);
                timer += Time.deltaTime;
                yield return null;
            }

            // Scale down smoothly
            timer = 0f;
            while (timer < scaleDownDuration)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, originalScale, timer / scaleDownDuration);
                timer += Time.deltaTime;
                yield return null;
            }

            transform.localScale = originalScale; // Ensure the scale is exactly the original scale at the end
            isScaling = false;
        }
    }
}
