using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeField] private Transform _mButton;

    public void OnClick()
    {
        StartCoroutine(Scale());
    }

    IEnumerator Scale()
    {
        _mButton.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        yield return new WaitForSeconds(0.1f);
        _mButton.localScale = new Vector3(1f, 1f, 1f);
    }
}
