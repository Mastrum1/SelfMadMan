using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouReBannedGameManager : MiniGameManager
{
    [SerializeField] private Transform _mSpawnPosition;
    private float _mAverageSpawnRate;
    private int _mCount;
    private bool _mIsEnd;
    private int _mNbDeleted;
    void Start()
    {
        _mAverageSpawnRate = 1.0f;
        _mCount = 0;
        StartCoroutine(SpawnComments());
        _mIsEnd = false;
        _mNbDeleted = 0;
    }

    void Update()
    {
        bool mStatus = true;
        if (_mTimer.timerValue != 0)
            return;
        if (_mIsEnd)
            return;
        /*CommentSpawner.CommentSharedInstance.StopAllComments();
        List<GameObject> mComments = CommentSpawner.CommentSharedInstance.GetActiveComments();
        for (int i = 0; i < mComments.Count; i++) {
            DisplayTikTokComment mDisplayComment = mComments[i].GetComponent<DisplayTikTokComment>();
            mDisplayComment.DeleteComment -= OnDeleteComment;
            mDisplayComment.ExitScreen -= OnScreenExited;
            if (!mDisplayComment.GetIsGood())
                mStatus  = false;
            mComments[i].SetActive(false);
        }
        EndGame(mStatus);*/
    }

    void EndGame(bool status)
    {
        EndMiniGame(status, miniGameScore);
        CommentSpawner.CommentSharedInstance.StopAllComments();
        _mIsEnd = true;
    }

    IEnumerator  SpawnComments()
    {
        while (/*_mTimer.timerValue >= 420 &&*/ !_mIsEnd) {
            yield return new WaitForSeconds((_mCount == 0) ? 0 : _mAverageSpawnRate);
            GameObject mComment = CommentSpawner.CommentSharedInstance.GetPooledComment();
            _mCount++;
            if (mComment != null && !_mIsEnd) {
                mComment.SetActive(true);
                DisplayTikTokComment mDisplayComment = mComment.GetComponent<DisplayTikTokComment>();
                mComment.transform.position = _mSpawnPosition.position;
                mDisplayComment.ResetComment();
                mDisplayComment.DeleteComment += OnDeleteComment;
                mDisplayComment.ExitScreen += OnScreenExited;
                if (_mNbDeleted > 0)
                    mComment.GetComponent<CommentMovement>().MoveFaster(_mSpawnPosition.position + new Vector3(0, 10, 0), _mNbDeleted);

            }
        }
    }

    void SpawnComment()
    {
        GameObject mComment = CommentSpawner.CommentSharedInstance.GetPooledComment();
        _mCount++;
        if (mComment != null && !_mIsEnd) {
            mComment.SetActive(true);
            DisplayTikTokComment mDisplayComment = mComment.GetComponent<DisplayTikTokComment>();
            mComment.transform.position = _mSpawnPosition.position;
            mDisplayComment.ResetComment();
            mDisplayComment.DeleteComment += OnDeleteComment;
            mDisplayComment.ExitScreen += OnScreenExited;
        }
    }

    public void OnDeleteComment(bool IsGood, GameObject Comment)
    {
        // call all the active comment and send my pos to them
        _mNbDeleted++;
       // SpawnComment();
        CommentSpawner.CommentSharedInstance.AccelarateActiveComments(Comment.transform.position);
        Comment.SetActive(false);
        DisplayTikTokComment mDisplayTikTokComment = Comment.GetComponent<DisplayTikTokComment>();
        mDisplayTikTokComment.DeleteComment -= OnDeleteComment;
        mDisplayTikTokComment.ExitScreen -= OnScreenExited;
        if (IsGood)
            EndGame(false);
    }

    public void OnScreenExited(bool IsGood, GameObject Comment)
    {
        Comment.SetActive(false);
        DisplayTikTokComment mDisplayTikTokComment = Comment.GetComponent<DisplayTikTokComment>();
        mDisplayTikTokComment.DeleteComment -= OnDeleteComment;
        mDisplayTikTokComment.ExitScreen -= OnScreenExited;
        if (!IsGood)
            EndGame(false);
    }
}
