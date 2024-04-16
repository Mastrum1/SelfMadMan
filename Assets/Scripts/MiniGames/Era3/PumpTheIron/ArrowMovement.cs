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
    }
    void Update()
    {
        switch (GameManager.instance.FasterLevel)
        {
            case 1:
                _mFasterLevel = GameManager.instance.FasterLevel;
                break;
            case 2:
                _mFasterLevel = (GameManager.instance.FasterLevel / 1.25f);
                break;
            case 3:
                _mFasterLevel = (GameManager.instance.FasterLevel / 1.45f);
                break;
            default:
                break;
        }
        transform.Translate(Vector3.down * _mSpeed * Time.deltaTime * GameManager.instance.FasterLevel);
    }
}
