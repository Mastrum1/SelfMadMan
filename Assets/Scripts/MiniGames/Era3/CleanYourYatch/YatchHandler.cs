using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Video;

public class YatchHandler : MonoBehaviour
{
    public  event Action GarbageDeleted;

    [SerializeField] List<VideoPlayer> _mPloufVideos;
    [SerializeField] List<ParticleSystem> _mParticles;

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("GameInteractable")) {
            /*VideoPlayer videoPlayerplayer = GetVideoPlayer();
            videoPlayerplayer.gameObject.SetActive(true);
            videoPlayerplayer.gameObject.transform.position = other.gameObject.transform.position;
            videoPlayerplayer.Play();
            StartCoroutine(ResetVideo(videoPlayerplayer));*/
            StartCoroutine(ScaleDown(other.gameObject));
            //other.gameObject.SetActive(false);
            GarbageDeleted?.Invoke();

            /*ParticleSystem mParticle = GetParticle();
            mParticle.gameObject.SetActive(true);
            mParticle.gameObject.transform.position = other.gameObject.transform.position;
            mParticle.Play();
            StartCoroutine(ResetParticle(mParticle));
            other.gameObject.SetActive(false);
            GarbageDeleted?.Invoke();*/
        }
    }

    IEnumerator ScaleDown(GameObject obj)
    {
        int i = 0;
        while (i < 3) {
            obj.transform.localScale /= 1.5f;
            i++;
            yield return new WaitForSeconds(0.15f);
        }
        VideoPlayer videoPlayerplayer = GetVideoPlayer();
        videoPlayerplayer.gameObject.SetActive(true);
        videoPlayerplayer.gameObject.transform.position = obj.gameObject.transform.position;
        videoPlayerplayer.Play();
        StartCoroutine(ResetVideo(videoPlayerplayer));
        obj.SetActive(false);
    }

    IEnumerator ResetVideo(VideoPlayer video)
    {
        yield return new WaitForSeconds(0.5f);
        video.Stop();
        video.gameObject.SetActive(false);
    }

    IEnumerator ResetParticle(ParticleSystem particle)
    {
        yield return new WaitForSeconds(0.25f);
        particle.gameObject.SetActive(false);
    }

    VideoPlayer GetVideoPlayer()
    {
        for (int i = 0; i < _mPloufVideos.Count; i++)
            if (!_mPloufVideos[i].gameObject.activeInHierarchy)
                return _mPloufVideos[i];
        return null;
    }

    ParticleSystem GetParticle()
    {
        for (int i = 0; i < _mPloufVideos.Count; i++)
            if (!_mPloufVideos[i].gameObject.activeInHierarchy)
                return _mParticles[i];
        return null;
    }

    
}
