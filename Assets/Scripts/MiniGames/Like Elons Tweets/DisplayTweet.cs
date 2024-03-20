using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayTweet : MonoBehaviour
{
    [SerializeField] TweetScriptableObject TweetData;
    [SerializeField] TMP_Text TweetContent;
    [SerializeField] TMP_Text NumberOfRetweet;
    [SerializeField] TMP_Text NumberOfComment;
    [SerializeField] SpriteRenderer ProfilPicture;

    // Start is called before the first frame update
    void Start()
    {
        TweetContent.text = TweetData.TweetContent;
        ProfilPicture.sprite = TweetData.ProfilPicture;
        NumberOfRetweet.text = TweetData.NumberOfRetweets.ToString();
        NumberOfComment.text = TweetData.NumberOfComments.ToString();
    }
}
