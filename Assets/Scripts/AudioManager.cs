using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource MusicSource, SFXSource;
    public AudioClip[] musicClips, SFXClip;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize AudioSource components if they are not set in the Inspector
        if (MusicSource == null)
        {
            MusicSource = gameObject.AddComponent<AudioSource>();
        }
        if (SFXSource == null)
        {
            SFXSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Method to play music clip
    public void PlayMusic(int index)
    {
        if (index < musicClips.Length && index >= 0)
        {
            MusicSource.clip = musicClips[index];
            MusicSource.Play();
        }
        else
        {
            Debug.LogWarning("Invalid music index: " + index);
        }
    }

    // Method to play sound effect clip
    public void PlaySFX(int index)
    {
        if (index < SFXClip.Length && index >= 0)
        {
            SFXSource.clip = SFXClip[index];
            SFXSource.Play();
        }
        else
        {
            Debug.LogWarning("Invalid SFX index: " + index);
        }
    }

    // Method to stop playing music
    public void StopMusic()
    {
        MusicSource.Stop();
    }

    // Method to stop playing sound effects
    public void StopSFX()
    {
        SFXSource.Stop();
    }

    // Method to modify the volume of music
    public void SetMusicVolume(float volume)
    {
        MusicSource.volume = Mathf.Clamp01(volume);
    }

    // Method to modify the volume of sound effects
    public void SetSFXVolume(float volume)
    {
        SFXSource.volume = Mathf.Clamp01(volume);
    }

    // Method to fade music volume in or out over time
    public IEnumerator FadeMusic(float targetVolume, float fadeDuration)
    {
        float startVolume = MusicSource.volume;
        float startTime = Time.time;

        while (Time.time < startTime + fadeDuration)
        {
            MusicSource.volume = Mathf.Lerp(startVolume, targetVolume, (Time.time - startTime) / fadeDuration);
            yield return null;
        }

        MusicSource.volume = targetVolume;
    }

    // Method to fade sound effects volume in or out over time
    public IEnumerator FadeSFX(float targetVolume, float fadeDuration)
    {
        float startVolume = SFXSource.volume;
        float startTime = Time.time;

        while (Time.time < startTime + fadeDuration)
        {
            SFXSource.volume = Mathf.Lerp(startVolume, targetVolume, (Time.time - startTime) / fadeDuration);
            yield return null;
        }

        SFXSource.volume = targetVolume;
    }

}