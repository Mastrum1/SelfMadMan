using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager _mInstance;

    [SerializeField] private AudioSource _mMusicSource;
    [SerializeField] private AudioSource _mSfxSource;
    [SerializeField] private AudioClip[] _mMusicClips;
    [SerializeField] private AudioClip[] _mSfxClip;

    public static AudioManager Instance
    {
        get { return _mInstance; }
    }

    public AudioSource MusicSource { get { return _mMusicSource; }  }
    public AudioSource SFXSource { get { return _mSfxSource; } }
    public AudioClip[] MusicClips { get { return _mMusicClips; } }
    public AudioClip[] SFXClips { get { return _mSfxClip; } }

    private void Awake()
    {
        // Singleton pattern to ensure only one instance existsfffff
        if (_mInstance == null)
        {
            _mInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize AudioSource components if they are not set in the Inspector
        if (_mMusicSource == null)
        {
            _mMusicSource = gameObject.AddComponent<AudioSource>();
        }
        if (_mSfxSource == null)
        {
            _mSfxSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Method to play music clip
    public void PlayMusic(int index)
    {
        if (index < _mMusicClips.Length && index >= 0)
        {
            _mSfxSource.clip = _mMusicClips[index];
            _mSfxSource.Play();
        }
        else
        {
            Debug.LogWarning("Invalid music index: " + index);
        }
    }

    // Method to play sound effect clip
    public void PlaySFX(int index)
    {
        if (index < _mSfxClip.Length && index >= 0)
        {
            _mSfxSource.clip = _mSfxClip[index];
            _mSfxSource.Play();
        }
        else
        {
            Debug.LogWarning("Invalid SFX index: " + index);
        }
    }

    // Method to stop playing music
    public void StopMusic()
    {
        _mSfxSource.Stop();
    }

    // Method to stop playing sound effects
    public void StopSFX()
    {
        _mSfxSource.Stop();
    }

    // Method to modify the volume of music
    public void SetMusicVolume(float volume)
    {
        _mSfxSource.volume = Mathf.Clamp01(volume);
    }

    // Method to modify the volume of sound effects
    public void SetSFXVolume(float volume)
    {
        _mSfxSource.volume = Mathf.Clamp01(volume);
    }

    // Method to fade music volume in or out over time
    public IEnumerator FadeMusic(float targetVolume, float fadeDuration)
    {
        float startVolume = _mSfxSource.volume;
        float startTime = Time.time;

        while (Time.time < startTime + fadeDuration)
        {
            _mSfxSource.volume = Mathf.Lerp(startVolume, targetVolume, (Time.time - startTime) / fadeDuration);
            yield return null;
        }

        _mSfxSource.volume = targetVolume;
    }

    // Method to fade sound effects volume in or out over time
    public IEnumerator FadeSFX(float targetVolume, float fadeDuration)
    {
        float startVolume = _mSfxSource.volume;
        float startTime = Time.time;

        while (Time.time < startTime + fadeDuration)
        {
            _mSfxSource.volume = Mathf.Lerp(startVolume, targetVolume, (Time.time - startTime) / fadeDuration);
            yield return null;
        }

        _mSfxSource.volume = targetVolume;
    }

    private void Start()
    {
        PlayMusic(0);
    }
}