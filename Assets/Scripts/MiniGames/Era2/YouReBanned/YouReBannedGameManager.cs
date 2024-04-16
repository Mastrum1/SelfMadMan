using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouReBannedGameManager : MiniGameManager
{
    [SerializeField] float _mMinimalSpawnTime;
    [SerializeField] List<GameObject> _mComments;
    [SerializeField] List<DisplayTikTokComment> _mTikTokComments;
    [SerializeField] List<CommentMovement> _mCommentMovement;

    public override void Awake()
    {
        base.Awake();
        _mCommentMovement[4].ExitScreen += OnScreenExited;
        foreach (DisplayTikTokComment comment in _mTikTokComments)
        {
            comment.DeleteComment += OnDeleteComment;
        }
    }

    void Start()
    {
        StartCoroutine(SpawnComment());
    }

    void Update()
    {
        bool mStatus = true;
        if (_mTimer.TimerValue != 0)
            return;

        //EndMiniGame(mStatus, miniGameScore);
    }

    public void OnDeleteComment(bool IsGood, GameObject Comment)
    {
        Comment.SetActive(false);
        if (IsGood)
            EndMiniGame(false, miniGameScore);
    }

    public void OnScreenExited(bool IsGood, GameObject Comment)
    {
        Comment.SetActive(false);
        if (!IsGood)
            EndMiniGame(false, miniGameScore);
    }

    IEnumerator SpawnComment()
    {
        while (_mTimer.TimerValue != 0)
        {
            yield return new WaitForSeconds(Random.Range(0.5f+_mMinimalSpawnTime, 1.5f+_mMinimalSpawnTime));
            if (!_mComments[0].activeSelf)
            {
                _mComments[0].SetActive(true);
                _mTikTokComments[0].enabled = true;
                _mTikTokComments[0].IsEnable = true;
                _mTikTokComments[0].ChangeProfile();
            }
            else
            {
                for (int i = _mComments.Count-1 ; i >= 0; i--)
                {
                    if (_mComments[i].activeSelf)
                    {
                        _mCommentMovement[i].CommentMoveUp();
                    }
                }
                _mTikTokComments[0].ChangeProfile();
            }
        }
    }
}
