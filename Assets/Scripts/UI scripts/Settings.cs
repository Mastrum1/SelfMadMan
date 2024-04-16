using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private bool _mMusic;
    [SerializeField] private bool _mSound;

    [SerializeField] private Image _mMusicImage;
    [SerializeField] private Image _mSoundImage;
     
    [SerializeField] private Sprite _mONBG;
    [SerializeField] private Sprite _mOFFBG;

    private void Start()
    {
        _mMusic = true;
        _mSound = true;
    }

    public void ONOFFMusic()
    {
        if (_mMusic == true)
        {
            _mMusic = false;
            _mMusicImage.sprite = _mOFFBG;
            AudioManager.Instance.StopMusic();
        }
        else
        {
            _mMusic = true;
            _mMusicImage.sprite = _mONBG;
            //AudioManager.Instance.PlayMusic();
        }
    }

    public void ONOFFSFX()
    {
        if (_mSound == true)
        {
            _mSound = false;
            _mSoundImage.sprite = _mOFFBG;
            AudioManager.Instance.StopSFX();
        }
        else
        {
            _mSound = true;
            _mSoundImage.sprite = _mONBG;
            //AudioManager.Instance.PlaySFX();
        }
    }
}
