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
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void FadeOut(ObstacleScript obj, RizzHerInteractableManager parent)
    {
        obj.transform.SetParent(parent.transform);
        gameObject.SetActive(false);
    }
}
