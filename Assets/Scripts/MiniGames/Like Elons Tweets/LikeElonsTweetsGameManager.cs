using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LikeElonsTweetsGameManager : MiniGameManager
{
    [SerializeField] Transform SpawnPosition;

    private float _mAverageSpawnRate;
    
    // Start is called before the first frame update
    void Start()
    {
        _mAverageSpawnRate = 0.5f;
        StartCoroutine(SpawnTweet());
    }

    // Update is called once per frame
    void Update()
    {
        bool mStatus = true;
        if (_mTimer.timerValue != 0)
            return;
        TweetSpawner.SharedInstance.StopAllTweetes();
        List<GameObject> mTweets = TweetSpawner.SharedInstance.GetActiveTweets();
        for (int i = 0; i < mTweets.Count; i++) {
            if (!mTweets[i].GetComponent<DisplayTweet>().FinalStatus())
                mStatus = false;
            mTweets[i].GetComponent<DisplayTweet>().LikeTweet -= OnLikeTweet;
            mTweets[i].GetComponent<DisplayTweet>().ExitScreen -= OnScreenExited;
        }
        EndMiniGame(mStatus, miniGameScore);
    }

    IEnumerator  SpawnTweet()
    { 
        while (_mTimer.timerValue >= 420) {
            yield return new WaitForSeconds(_mAverageSpawnRate);
            GameObject mTweet = TweetSpawner.SharedInstance.GetPooledTweet();
            if (mTweet != null) {
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
        if (!IsElon)
            EndMiniGame(false, miniGameScore);
    }

    void OnScreenExited(bool IsElon, GameObject Tweet)
    {
        if (IsElon)
            EndMiniGame(false, miniGameScore);
        Tweet.SetActive(false);
        Tweet.GetComponent<DisplayTweet>().Disable();
        Tweet.GetComponent<DisplayTweet>().LikeTweet -= OnLikeTweet;
        Tweet.GetComponent<DisplayTweet>().ExitScreen -= OnScreenExited;
    }
}
