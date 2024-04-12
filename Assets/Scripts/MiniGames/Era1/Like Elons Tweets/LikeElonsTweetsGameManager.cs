using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LikeElonsTweetsGameManager : MiniGameManager
{
    [SerializeField] Transform SpawnPosition;

    [SerializeField] private float _mAverageSpawnRate;
    private float _mTotalTime;
    bool _mIsEnd;
    int _mCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        _mAverageSpawnRate = 0.75f;
        _mIsEnd = false;
        StartCoroutine(SpawnTweet());
    }

    // Update is called once per frame
    public override void Update()
    {
        bool mStatus = true;
        if (_mTimer.TimerValue == 0 || _mIsEnd) {
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
    }

    void EndGame(bool status)
    {
        if (!_mIsEnd) {
            TweetSpawner.SharedInstance.StopAllTweetes();
            _mIsEnd = true;
            EndMiniGame(status, miniGameScore);
        }
    }


    IEnumerator SpawnTweet()
    { 
        while (_mTimer.TimerValue > 1.0f && !_mIsEnd) {
            yield return new WaitForSeconds(_mCount == 0 ? 0 :  _mAverageSpawnRate);
            GameObject mTweet = TweetSpawner.SharedInstance.GetPooledTweet();
            if (mTweet != null && !_mIsEnd) {
                DisplayTweet mDisplayTweet = mTweet.GetComponent<DisplayTweet>();
                mTweet.SetActive(true);
                mTweet.transform.position = SpawnPosition.position;
                mDisplayTweet.ResetTweet();
                mDisplayTweet.LikeTweet += OnLikeTweet;
                mDisplayTweet.ExitScreen += OnScreenExited;
                _mCount++;
            }
        }
    }

    void OnLikeTweet(bool IsElon)
    {
        if (!IsElon && !_mIsEnd)
            EndGame(false);
        
        else Amount++;
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
