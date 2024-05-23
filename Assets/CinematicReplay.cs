using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CinematicReplay : MonoBehaviour
{
    [SerializeField] private VideoPlayer _Video;
    [SerializeField] private GameObject _borders;


    public void Update()
    {
        if (_Video.isPaused)
        {
            _borders.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    public void PlayVideo()
    {
        StartCoroutine(PrepareVideo());
    }


    public IEnumerator PrepareVideo()
    {
        _Video.Prepare();
        while (!_Video.isPrepared)
            yield return new WaitForEndOfFrame();
        _Video.frame = 0;
        _Video.Play();
        _borders.SetActive(true);
    }
}
