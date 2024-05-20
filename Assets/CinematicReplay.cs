using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CinematicReplay : MonoBehaviour
{
    [SerializeField] private VideoPlayer _Video;
    [SerializeField] private BoxCollider2D _Collider;

    private double loopTime;
    private bool HasReachedPoint = false;

    public void Start()
    {
        loopTime = (_Video.clip.length * 72.15f) / 100.0f;
    }

    public void Update()
    {
        if (_Video.time >= loopTime && !HasReachedPoint)
        {
            HasReachedPoint = true;
            _Collider.enabled = false;
            _Video.time = loopTime;
        }
    }

    private void OnEnable()
    {
        PlayVideo();
    }

    public void PlayVideo()
    {
        _Video.Play();
        _Collider.enabled = true;
    }
}
