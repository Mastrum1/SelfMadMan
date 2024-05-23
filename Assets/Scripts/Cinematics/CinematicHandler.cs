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
    public event Action OnVideoPrepared;
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
        StartCoroutine(PrepareVideo());
    }

    public IEnumerator PrepareVideo()
    {
        player.Prepare();
        while (!player.isPrepared)
            yield return new WaitForEndOfFrame();
        player.frame = 0;
        player.Play();
        OnVideoPrepared?.Invoke();
    }
    void OnVideoEnd(VideoPlayer vp)
    {
        player.Play();
        player.time = loopTime;
    }
}
