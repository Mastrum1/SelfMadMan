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

    [ContextMenu("Move Up")]
    public void CommentMoveUp()
    {
        if (_mNextComment)
        {
            if (!_mNextComment.activeSelf)
                _mNextComment.SetActive(true);

            StartCoroutine(WaitAnim());
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
        _mCommentPreviousComment.UpdateComment();
    }

    public void StartAnim()
    {
        _mCommentNextComment.ActualBackground = _mActualComment.ActualBackground;
        _mCommentNextComment.Girls = _mActualComment.Girls;
        _mCommentNextComment.SelectedData = _mActualComment.SelectedData;


        _mAnimator.SetTrigger("Up");
    }

    IEnumerator WaitAnim()
    {
        if (gameObject.name != "Comment_01_Anim")
            yield return new WaitForSeconds(0.48f); 
        _mCommentNextComment.UpdateComment();
        StopCoroutine(WaitAnim());
    }
}
