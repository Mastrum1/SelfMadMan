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
    [SerializeField] TMP_Text NumberOfRetweet;
    [SerializeField] TMP_Text NumberOfComment;
    [SerializeField] SpriteRenderer ProfilPicture;
    [SerializeField] SpriteRenderer Like;
    [SerializeField] Sprite BaseState;
    [SerializeField] Sprite LikeState;
    bool mIsEnable;
    bool mIsLiked;

    public event Action<bool, GameObject> ExitScreen;
    public event Action<bool> LikeTweet;

    void Start()
    {
        mIsEnable = true;
        mIsLiked = false;
        TweetContent.text = TweetData.TweetContent;
        ProfilPicture.sprite = TweetData.ProfilPicture;
        NumberOfRetweet.text = TweetData.NumberOfRetweets.ToString();
        NumberOfComment.text = TweetData.NumberOfComments.ToString();
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
            mIsLiked = true;
            Like.sprite = LikeState;
            LikeTweet?.Invoke(TweetData.IsElon);
        }
    }

    public void ResetTweet()
    {
        Like.sprite = BaseState;
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
