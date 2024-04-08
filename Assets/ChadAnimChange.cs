using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ChadAnimChange : MonoBehaviour
{

    [SerializeField] private VideoClip[] _mVideoClips;
    [SerializeField] private VideoPlayer _mVideoPlayer;

    void Start()
    {
        _mVideoPlayer.clip = _mVideoClips[GameManager.instance.Era];
    }

}
