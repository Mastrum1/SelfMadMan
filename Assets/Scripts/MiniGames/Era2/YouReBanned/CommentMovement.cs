using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Metadata;

public class CommentMovement : MonoBehaviour
{
    [SerializeField] GameObject _mNextComment;
    [SerializeField] DisplayTikTokComment _mCommentNextComment;
    [SerializeField] GameObject _mPreviousComment;
    [SerializeField] DisplayTikTokComment _mCommentPreviousComment;
    [SerializeField] DisplayTikTokComment _mActualComment;
    [SerializeField] Animator _mAnimator;

    public event Action<bool, GameObject> ExitScreen;

    void Start()
    {
        
    }

    void Update()
    {
    }

    [ContextMenu("Move Up")]
    public void CommentMoveUp()
    {
        if (_mNextComment)
        {

            _mAnimator.SetTrigger("Up");
            StartCoroutine(MoveUpNext());

            _mCommentNextComment.ActualBackground = _mActualComment.ActualBackground;
            _mCommentNextComment.Girls = _mActualComment.Girls;
            _mCommentNextComment.SelectedData = _mActualComment.SelectedData;
        }
        else
        {
            Debug.Log(_mActualComment.SelectedData.IsGood);
            ExitScreen?.Invoke(_mActualComment.SelectedData.IsGood, gameObject);
        }
    }

    [ContextMenu("Move Down")]
    public void CommentMoveDown()
    {
        _mCommentPreviousComment.ActualBackground = _mActualComment.ActualBackground;
        _mCommentPreviousComment.Girls = _mActualComment.Girls;
        _mCommentPreviousComment.SelectedData = _mActualComment.SelectedData;
        StartCoroutine(MoveUpPrevious());
    }

    IEnumerator MoveUpNext()
    {
        yield return new WaitForSeconds(.15f);
        if (!_mNextComment.activeSelf)
            _mNextComment.SetActive(true);
        _mCommentNextComment.StartCoroutine(_mCommentNextComment.UpdateComment());
    }    
    IEnumerator MoveUpPrevious()
    {
        yield return new WaitForSeconds(.15f);
        _mCommentPreviousComment.StartCoroutine(_mCommentPreviousComment.UpdateComment());
    }

}
