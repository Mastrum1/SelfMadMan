using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpGameManager : MiniGameManager
{
    [SerializeField] PopUpSpawner _mSpawner;
    [SerializeField] Animator _mAnimator;
    bool _enabled = false;
    bool _mIsEnd = false;

    // Update is called once per frame
    public void OnDownload(GameObject button)
    {
        if (!_mSpawner.IsActivePopUp() && !_mIsEnd) {
            _mAnimator.SetTrigger("PopupPress");
            EndGame(true);
        }
    }

    void EndGame(bool status)
    {
        if (!_mIsEnd) {
            _mIsEnd = true;
            _mSpawner.DisablePopUp();
            EndMiniGame(status, miniGameScore);
        }
    }

    public override void Update()
    {
        if (_mTimer.TimerValue == 0 && !_mIsEnd) {
            _mIsEnd = true;
            _mSpawner.DisablePopUp();
        }
        base.Update();
        if (!_mSpawner.IsActivePopUp()) {
            if (!_enabled) _mAnimator.SetTrigger("NoPopup");
            _enabled = true;
        }
    }
}
