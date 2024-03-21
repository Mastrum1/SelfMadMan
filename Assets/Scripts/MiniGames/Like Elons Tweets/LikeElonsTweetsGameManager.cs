using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LikeElonsTweetsGameManager : MiniGameManager
{
    [SerializeField] Vector3 SpawnPosition;
    private float _mAverageSpawnRate;

    // Start is called before the first frame update
    void Start()
    {
        _mAverageSpawnRate = GameManager.instance.Speed / 8;
        StartCoroutine(SpawnTweet());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator  SpawnTweet()
    {
        while (_mTimer.timerValue != 0) {
            //float nextDelay = Random.Range(0, _mAverageSpawnRate);
            yield return new WaitForSeconds(_mAverageSpawnRate);
            GameObject tweet = TweetSpawner.SharedInstance.GetPooledTweet(); 
            if (tweet != null) {
               // tweet.transform.position = SpawnPosition;
                tweet.SetActive(true);
            }
        }
    }
}
