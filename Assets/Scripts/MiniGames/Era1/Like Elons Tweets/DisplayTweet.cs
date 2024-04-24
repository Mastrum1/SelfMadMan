using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class DisplayTweet : MonoBehaviour
{
    [SerializeField] TweetScriptableObject TweetData;
    [SerializeField] TMP_Text TweetContent;
    [SerializeField] TMP_Text ProfileName;
    [SerializeField] public Transform Bottom;

    [SerializeField] Image ProfilPicture;
    [SerializeField] SpriteRenderer Like;
    [SerializeField] Sprite _mBaseState;
    [SerializeField] Sprite _mGoodLikeState;
    [SerializeField] Sprite _mBadLikeState;
    [SerializeField] LikeButton _mLikeButton;
    bool mIsEnable;
    bool mIsLiked;

    public event Action<bool, GameObject> ExitScreen;
    public event Action<bool> LikeTweet;

    void Start()
    {
        mIsEnable = true;
        mIsLiked = false;
        TweetContent.text = TweetData.TweetContent + "<line-height=125%>\n<size=0.7em><color=#687684>" + TweetData.Date + "</color></size>" ;
        ProfilPicture.sprite = TweetData.ProfilPicture;
        ProfileName.text = TweetData.ProfileName + "<font-weight=\"300\"><color=#687684> " + TweetData.Pseudo + "</color></font-weight>";
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("GameInteractable")) {
            ExitScreen?.Invoke(!TweetData.IsElon? false : TweetData.IsElon && mIsLiked ? false : true, this.transform.gameObject);
            ResetTweet();
        }
    }

    public void OnLikeTweet()
    {
        if (mIsEnable) {
            _mLikeButton.OnClick();
            mIsLiked = true;
            Like.sprite = (TweetData.IsElon) ? _mGoodLikeState : _mBadLikeState;
            LikeTweet?.Invoke(TweetData.IsElon);
        }
    }

    public void ResetTweet()
    {
        Like.sprite = _mBaseState;
        mIsEnable = true;
        mIsLiked = false;
    }

    public void Disable()
    {
        mIsEnable = false;
    }

    public bool FinalStatus()
    {
        if (TweetData.IsElon && !mIsLiked || !TweetData.IsElon && mIsLiked)
            return false;
        return true;
    }    
}
