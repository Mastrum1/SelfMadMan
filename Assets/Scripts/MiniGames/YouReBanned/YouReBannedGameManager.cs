using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouReBannedGameManager : MiniGameManager
{
    [SerializeField] private Transform _mSpawnPosition;
    private float _mAverageSpawnRate;
    void Start()
    {
        _mAverageSpawnRate = 1.0f;
        StartCoroutine(SpawnComments());
    }

    void Update()
    {
        bool mStatus = true;
        if (_mTimer.timerValue != 0)
            return;
        CommentSpawner.CommentSharedInstance.StopAllComments();
        List<GameObject> mComments = CommentSpawner.CommentSharedInstance.GetActiveComments();
        for (int i = 0; i < mComments.Count; i++) {
            //if (!mComments[i].GetComponent<>().FinalStatus())
              //  mStatus = false;
            DisplayTikTokComment mDisplayComment = mComments[i].GetComponent<DisplayTikTokComment>();
            mDisplayComment.DeleteComment -= OnDeleteComment;
            mDisplayComment.ExitScreen -= OnScreenExited;
        }
       // EndMiniGame(mStatus, miniGameScore);
    }

    IEnumerator  SpawnComments()
    { 
        while (_mTimer.timerValue >= 420) {
            yield return new WaitForSeconds(_mAverageSpawnRate);
            GameObject mComment = CommentSpawner.CommentSharedInstance.GetPooledComment();
            if (mComment != null) {
                mComment.SetActive(true);
                DisplayTikTokComment mDisplayComment = mComment.GetComponent<DisplayTikTokComment>();
               mComment.transform.position = _mSpawnPosition.position;
               mDisplayComment.ResetComment();
                mDisplayComment.DeleteComment += OnDeleteComment;
                mDisplayComment.ExitScreen += OnScreenExited;
            }
        }
    }

    public void OnDeleteComment(bool IsGood, GameObject Comment)
    {
        // IS GOOD CHECK

        Comment.SetActive(false);
        DisplayTikTokComment mDisplayTikTokComment = Comment.GetComponent<DisplayTikTokComment>();
        mDisplayTikTokComment.DeleteComment -= OnDeleteComment;
        mDisplayTikTokComment.ExitScreen -= OnScreenExited;
    }

    public void OnScreenExited(bool IsGood, GameObject Comment)
    {
        // IS GOOD CHECK

        Comment.SetActive(false);
        DisplayTikTokComment mDisplayTikTokComment = Comment.GetComponent<DisplayTikTokComment>();
        mDisplayTikTokComment.DeleteComment -= OnDeleteComment;
        mDisplayTikTokComment.ExitScreen -= OnScreenExited;
    }
}
