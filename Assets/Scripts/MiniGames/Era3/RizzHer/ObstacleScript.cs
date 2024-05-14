using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField] private float _mMaxTimeFadeOut = 0.1f;

    [SerializeField] private SpriteRenderer _mSpriteRenderer;

    private void OnEnable()
    {
        _mSpriteRenderer.color = new Color(_mSpriteRenderer.color.r, _mSpriteRenderer.color.g, _mSpriteRenderer.color.b, 255);
    }

    public void FadeOut(ObstacleScript obj, RizzHerInteractableManager parent)
    {
        StartCoroutine(FadeOutCoroutine(obj, parent));
    }

    private IEnumerator FadeOutCoroutine(ObstacleScript obj, RizzHerInteractableManager parent)
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
        obj.transform.SetParent(parent.transform);
        gameObject.SetActive(false);
    }
}
