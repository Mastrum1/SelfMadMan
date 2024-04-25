using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Video;

public class YatchHandler : MonoBehaviour
{
    public  event Action GarbageDeleted;

    [SerializeField] List<VideoPlayer> _mPloufVideos;

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("GameInteractable")) {
            VideoPlayer videoPlayerplayer = GetVideoPlayer();
            videoPlayerplayer.gameObject.SetActive(true);
            videoPlayerplayer.gameObject.transform.position = other.gameObject.transform.position;
            videoPlayerplayer.Play();
            StartCoroutine(ResetVideo(videoPlayerplayer));
            other.gameObject.SetActive(false);
            GarbageDeleted?.Invoke();
        }
    }

    IEnumerator ResetVideo(VideoPlayer video)
    {
        yield return new WaitForSeconds(0.5f);
        video.Stop();
        video.gameObject.SetActive(false);
    }

    VideoPlayer GetVideoPlayer()
    {
        for (int i = 0; i < _mPloufVideos.Count; i++)
            if (!_mPloufVideos[i].gameObject.activeInHierarchy)
                return _mPloufVideos[i];
        return null;
    }

    
}
