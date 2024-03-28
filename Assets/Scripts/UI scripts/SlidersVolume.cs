using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlidersVolume : MonoBehaviour
{
    [SerializeField] Slider _mMusicVolumeSlider;
    [SerializeField] Slider _mSFXVolumeSlider;

    public void SetVolumeMusic()
    {
        AudioManager.Instance.MusicSource.volume = _mMusicVolumeSlider.value;
    }

    public void SetVolumeSFX()
    {
        AudioManager.Instance.SFXSource.volume = _mSFXVolumeSlider.value;
    }
}
