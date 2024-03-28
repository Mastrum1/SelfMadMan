using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Tweet", menuName = "Tweet")]
public class TweetScriptableObject : ScriptableObject
{
    public string TweetContent;
    public Sprite ProfilPicture;
    public int NumberOfRetweets;
    public int NumberOfComments;
    public bool IsElon;
}
