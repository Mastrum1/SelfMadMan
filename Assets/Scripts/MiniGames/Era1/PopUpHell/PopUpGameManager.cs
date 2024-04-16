using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpGameManager : MiniGameManager
{
    [SerializeField] PopUpSpawner spawner;
    [SerializeField] Animator _mAnimator;
    bool _enabled = false;

    // Update is called once per frame
    public void OnDownload(GameObject button)
    {
        if (!spawner.IsActivePopUp()) {
            _mAnimator.SetTrigger("PopupPress");
            EndMiniGame(true, miniGameScore);
        }
    }

    public override void Update()
    {
        base.Update();
        if (!spawner.IsActivePopUp())
        {
            if (!_enabled) _mAnimator.SetTrigger("NoPopup");
            _enabled = true;
        }
    }
}
