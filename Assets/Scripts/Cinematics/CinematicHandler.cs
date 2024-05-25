using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CinematicHandler : MonoBehaviour
{

    [SerializeField] private VideoPlayer player;
    private float loopPercentage = 72.15f;
    private double loopTime;
    private bool HasReachedPoint = false;
    private bool VideoIsPlaying = true;

    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject borders;

    private AsyncOperation asyncOperation;

    // Start is called before the first frame update
    private void Awake()
    {
        asyncOperation = SceneManager.LoadSceneAsync("HomePage");
        asyncOperation.allowSceneActivation = false;
        PlayVideo();
        StartCoroutine(WaitForSceneLoad());
    }

    private void ChangeCinematicStyle(VideoPlayer source, string message)
    {
        source.enabled = false;
        StartGame();
    }

    private IEnumerator WaitForSceneLoad()
    {
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    public void Update()
    {
        if(player.time >=  loopTime && !HasReachedPoint)
        {
            HasReachedPoint = true;
            panel.SetActive(true);
        }

        if (player.isPaused)
        {
            player.Play();
            player.time = loopTime;
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

        player.frame = 0;
        player.Play();
        borders.SetActive(true);
        yield return null;
    }
   

    public void StartGame()
    {
        Debug.Log("start games");
        GameManager.instance.Player.IntroPlayed = true;
        asyncOperation.allowSceneActivation = true;
    }

}
