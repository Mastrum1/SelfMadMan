using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LikeButton : MonoBehaviour
{
    [SerializeField] private Transform _mButton;
    Vector3 _mStartScale;

    void Start()
    {
        _mStartScale = _mButton.localScale;
    }

    public void OnClick()
    {
        StartCoroutine(Scale());
    }

    IEnumerator Scale()
    {
        _mButton.localScale = _mStartScale * 0.75f;
        yield return new WaitForSeconds(0.1f);
        _mButton.localScale = _mStartScale;
    }
}
