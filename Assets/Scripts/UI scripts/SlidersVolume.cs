using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidersVolume : MonoBehaviour
{
    [SerializeField] Slider _mMusicVolumeSlider;
    [SerializeField] Slider _mSFXVolumeSlider;

    GameManager _mGameManager;
    Player _mPlayer;

    public void Awake()
    {
        _mGameManager = GameManager.instance;
        _mPlayer = _mGameManager.GetComponent<Player>();

        //AudioManager.Instance.MusicSource.volume = _mPlayer.VolumeMusic;
        //AudioManager.Instance.SFXSource.volume = _mPlayer.VolumeMusic;


    }

    public void SetVolumeMusic()
    {
        AudioManager.Instance.MusicSource.volume = _mMusicVolumeSlider.value;
        //_mPlayer.ChangeMusicVolume(_mMusicVolumeSlider.value);
    }

    public void SetVolumeSFX()
    {
        AudioManager.Instance.SFXSource.volume = _mSFXVolumeSlider.value;
        //_mPlayer.ChangeSFXVolume(_mSFXVolumeSlider.value);

    }
}
