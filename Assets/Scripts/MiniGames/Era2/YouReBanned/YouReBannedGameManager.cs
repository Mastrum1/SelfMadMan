using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouReBannedGameManager : MiniGameManager
{
    [SerializeField] private Transform _mSpawnPosition;
    private float _mAverageSpawnRate;
    private int _mCount;
    private void Start()
    {
        _mAverageSpawnRate = 1.0f;
        _mCount = 0;
        StartCoroutine(SpawnComments());
    }

    void Update()
    {
        bool mStatus = true;
        if (_mTimer.TimerValue != 0)
            return;
        CommentSpawner.CommentSharedInstance.StopAllComments();
        List<GameObject> mComments = CommentSpawner.CommentSharedInstance.GetActiveComments();
        for (int i = 0; i < mComments.Count; i++) {
            DisplayTikTokComment mDisplayComment = mComments[i].GetComponent<DisplayTikTokComment>();
            mDisplayComment.DeleteComment -= OnDeleteComment;
            mDisplayComment.ExitScreen -= OnScreenExited;
            if (!mDisplayComment.GetIsGood())
                mStatus  = false;
            mComments[i].SetActive(false);
        }
        EndMiniGame(mStatus, miniGameScore);
    }

    IEnumerator  SpawnComments()
    {
        while (_mTimer.TimerValue >= 3) {
            yield return new WaitForSeconds((_mCount == 0) ? 0 : _mAverageSpawnRate);
            GameObject mComment = CommentSpawner.CommentSharedInstance.GetPooledComment();
            _mCount++;
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

    private void OnDeleteComment(bool IsGood, GameObject Comment)
    {
        Comment.SetActive(false);
        DisplayTikTokComment mDisplayTikTokComment = Comment.GetComponent<DisplayTikTokComment>();
        mDisplayTikTokComment.DeleteComment -= OnDeleteComment;
        mDisplayTikTokComment.ExitScreen -= OnScreenExited;
        if (IsGood)
            EndMiniGame(false, miniGameScore);
        
        else Amount++;
    }

    private void OnScreenExited(bool IsGood, GameObject Comment)
    {
        Comment.SetActive(false);
        DisplayTikTokComment mDisplayTikTokComment = Comment.GetComponent<DisplayTikTokComment>();
        mDisplayTikTokComment.DeleteComment -= OnDeleteComment;
        mDisplayTikTokComment.ExitScreen -= OnScreenExited;
        if (!IsGood)
            EndMiniGame(false, miniGameScore);
    }
}
