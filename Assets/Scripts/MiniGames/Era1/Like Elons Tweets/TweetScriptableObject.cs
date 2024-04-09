using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Tweet", menuName = "Tweet")]
public class TweetScriptableObject : ScriptableObject
{
    public string TweetContent;
    public string ProfileName;
    public string Pseudo;
    public string Date;
    public Sprite ProfilPicture;
    public string NumberOfLikes;
    public bool IsElon;
}
