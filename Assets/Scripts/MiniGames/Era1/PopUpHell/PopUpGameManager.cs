using UnityEngine;

public class PopUpGameManager : MiniGameManager
{
    [SerializeField] private PopUpSpawner _mSpawner;
    [SerializeField] private Animator _mAnimator;
    bool _enabled = false;
    bool _mIsEnd = false;
    private AudioManager _audioManager;

    private void Start()
    {
        _mSpawner.OnClosePopUp += PopUpClosed;
        _audioManager = AudioManager.Instance;
    }

    private void PopUpClosed()
    {
        Amount++;
    }

    public void OnDownload(GameObject button)
    {
        if (!_mSpawner.IsActivePopUp() && !_mIsEnd) {
            _audioManager.PlaySFX(0);
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
