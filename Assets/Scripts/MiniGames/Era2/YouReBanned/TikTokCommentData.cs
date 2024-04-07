using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TikTokComment_0", menuName = "TikTokComment")]

public class TikTokCommentData : ScriptableObject
{
    public string ProfileName;
    public string CommentContent;
    public Sprite ProfilPicture;
    public bool IsGood;
}
