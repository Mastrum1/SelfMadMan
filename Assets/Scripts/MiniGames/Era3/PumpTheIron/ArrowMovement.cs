using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    [SerializeField] int _mSpeed;
    float _mFasterLevel;
    [SerializeField] private SpriteRenderer _mSpriteRenderer;
    private void OnEnable()
    {
        _mSpriteRenderer.color = new Color(_mSpriteRenderer.color.r, _mSpriteRenderer.color.g, _mSpriteRenderer.color.b, 255);
        gameObject.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
    }
    void Update()
    {
        switch (GameManager.instance.FasterLevel)
        {
            case 1:
                _mFasterLevel = GameManager.instance.FasterLevel;
                break;
            default:
                _mFasterLevel = (GameManager.instance.FasterLevel / 1.75f);
                break;
        }
        transform.Translate(Vector3.down * _mSpeed * Time.deltaTime * _mFasterLevel);
    }
}
