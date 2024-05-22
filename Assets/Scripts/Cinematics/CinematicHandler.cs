using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CinematicHandler : MonoBehaviour
{

    [SerializeField] private VideoPlayer player;
    private float loopPercentage = 72.15f;
    private double loopTime;
    private bool HasReachedPoint = false;
    public event Action OnReachedPoint;
    // Start is called before the first frame update
    private void Awake()
    {
        player.loopPointReached += OnVideoEnd;
    }

    public void Update()
    {
        if(player.time >=  loopTime && !HasReachedPoint)
        {
            HasReachedPoint = true;
            OnReachedPoint?.Invoke();
        }
    }
    void Start()
    {
        loopTime = (player.clip.length * loopPercentage) / 100.0f;
    }
    
    public void PlayVideo()
    {
        player.Play();
        /*double startTime = (player.clip.length * 60) / 100.0f;
        player.time = startTime;*/
    }
    void OnVideoEnd(VideoPlayer vp)
    {
        player.Play();
        player.time = loopTime;
    }
}
