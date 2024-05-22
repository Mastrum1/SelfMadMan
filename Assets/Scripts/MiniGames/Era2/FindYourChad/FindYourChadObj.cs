using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FindYourChadObj : MonoBehaviour
{
    [SerializeField] private VFXScaleUp _vfxScaleUp;
    [SerializeField] private float scaleUpDuration = 0.25f;
    [SerializeField] private float scaleDownDuration = 0.25f;
    [SerializeField] private float scaleUpFactor = 1.3f;
    [SerializeField] private bool _mIsReal = false;

    private bool isScaling = false;
    private Vector3 originalScale;
    public bool Real
    {
        get { return _mIsReal; }
        set { _mIsReal = value; }
    }

    [SerializeField] private SpriteRenderer _SpriteRenderer;
    public SpriteRenderer SpriteRenderer
    {
        get { return _SpriteRenderer; }
        set { _SpriteRenderer = value; }
    }

    [SerializeField] private EdgeCollider2D _collider;
   public EdgeCollider2D Collider
    {
        get { return _collider; }
        set { _collider = value; }
    }

    public event Action<bool> OnGameEnd;

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
    }


    public void OnTap()
    {
        if (_mIsReal)
        {
            OnGameEnd?.Invoke(true);
            _vfxScaleUp.OnObjectClicked();

        }
        else
        {
            OnGameEnd?.Invoke(false);
            StartCoroutine(PulseObject());
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator PulseObject()
    {
        while (true)
        {
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
