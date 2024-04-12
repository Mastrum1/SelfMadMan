using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownloadBtnAnimatorScript : MonoBehaviour
{
    [SerializeField] GameObject _Popups;
    [SerializeField] Animator _mAnimator;

    private void Update()
    {
        if (_Popups.transform.childCount == 0)
        {
            Jiggle();
        }
    }
    void Jiggle()
    {
        _mAnimator.SetTrigger("NoPopup");
    }

    void Press()
    {
        _mAnimator.SetTrigger("PopupPress");
    }

}
