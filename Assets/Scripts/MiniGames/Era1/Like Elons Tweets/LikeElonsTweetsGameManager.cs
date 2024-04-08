using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LikeElonsTweetsGameManager : MiniGameManager
{
    [SerializeField] Transform SpawnPosition;

    [SerializeField] private float _mAverageSpawnRate;
    bool _mIsEnd;
    
    // Start is called before the first frame update
    void Start()
    {
        _mAverageSpawnRate = 0.75f;
        _mIsEnd = false;
        StartCoroutine(SpawnTweet());
    }

    // Update is called once per frame
    void Update()
    {
        bool mStatus = true;
        if (_mTimer.timerValue != 0 || _mIsEnd)
            return;
        TweetSpawner.SharedInstance.StopAllTweetes();
        List<GameObject> mTweets = TweetSpawner.SharedInstance.GetActiveTweets();
        for (int i = 0; i < mTweets.Count; i++) {
            if (!mTweets[i].GetComponent<DisplayTweet>().FinalStatus())
                mStatus = false;
            mTweets[i].GetComponent<DisplayTweet>().LikeTweet -= OnLikeTweet;
            mTweets[i].GetComponent<DisplayTweet>().ExitScreen -= OnScreenExited;
        }
        EndGame(mStatus);
    }

    void EndGame(bool status)
    {
        if (!_mIsEnd) {
            TweetSpawner.SharedInstance.StopAllTweetes();
            _mIsEnd = true;
            Debug.Log("ENDGAME  " + _mIsEnd);
            EndMiniGame(status, miniGameScore);
        }
    }


    IEnumerator  SpawnTweet()
    { 
        while (_mTimer.timerValue >= 420 && !_mIsEnd) {
            yield return new WaitForSeconds(_mAverageSpawnRate);
            GameObject mTweet = TweetSpawner.SharedInstance.GetPooledTweet();
            Debug.Log("Spawneeeeeeeeeer " + _mIsEnd);
            if (mTweet != null && !_mIsEnd) {
                DisplayTweet mDisplayTweet = mTweet.GetComponent<DisplayTweet>();
                mTweet.SetActive(true);
                mTweet.transform.position = SpawnPosition.position;
                mDisplayTweet.ResetTweet();
                mDisplayTweet.LikeTweet += OnLikeTweet;
                mDisplayTweet.ExitScreen += OnScreenExited;
            }
        }
    }

    void OnLikeTweet(bool IsElon)
    {
        if (!IsElon && !_mIsEnd)
            EndGame(false);
    }

    void OnScreenExited(bool IsElon, GameObject Tweet)
    {
        if (IsElon && !_mIsEnd)
            EndGame(false);
        Tweet.SetActive(false);
        Tweet.GetComponent<DisplayTweet>().Disable();
        Tweet.GetComponent<DisplayTweet>().LikeTweet -= OnLikeTweet;
        Tweet.GetComponent<DisplayTweet>().ExitScreen -= OnScreenExited;
    }
}
