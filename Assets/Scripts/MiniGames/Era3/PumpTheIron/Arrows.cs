using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrows : MonoBehaviour
{
    public event Action<bool, string> OnAction;

    [SerializeField] private string _mDir;
    [SerializeField] private SwipeDir _mSwipeDir;
    [SerializeField] private ExitArea _mExitArea;

    [SerializeField] private SpriteRenderer _mSpriteRenderer;
    [SerializeField] private BoxCollider2D _mEndZone;
    [SerializeField] private float _mMaxTimeFadeOut = 0.1f;

    [SerializeField] private float scaleUpDuration = 0.5f;
    [SerializeField] private float scaleUpFactor = 1.3f;

    private void OnEnable()
    {
        _mSwipeDir.OnSwipe += OnSwipe;
    }

    private void OnDisable()
    {
        _mSwipeDir.OnSwipe -= OnSwipe;

    }

    public void OnSwipe(string Dir)
    {
        if (Dir == _mDir)
        {
            StartCoroutine(ScaleObject());
            StartCoroutine(FadeOut());
            OnAction?.Invoke(true, Dir);
            _mExitArea.EndGame = true;
           
        }
    }

    private IEnumerator ScaleObject()
    {
        // Scale up to 1.3
        float timer = 0f;
        Vector3 targetScale = gameObject.transform.localScale * scaleUpFactor;

        while (timer < scaleUpDuration)
        {
            transform.localScale = Vector3.Lerp(gameObject.transform.localScale, targetScale, timer / scaleUpDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        StopCoroutine(ScaleObject());
    }

        private IEnumerator FadeOut()
    {
        float counter = 0;
        while (counter < _mMaxTimeFadeOut)
        {
            counter += Time.deltaTime;
            //Fade from 1 to 0
            float alpha = Mathf.Lerp(1, 0, counter / _mMaxTimeFadeOut);

            _mSpriteRenderer.color = new Color(_mSpriteRenderer.color.r, _mSpriteRenderer.color.g, _mSpriteRenderer.color.b, alpha);

            //Wait for a frame
            yield return null;
        }

        gameObject.SetActive(false);
        StopCoroutine(FadeOut());
        enabled = false;

    }

}
