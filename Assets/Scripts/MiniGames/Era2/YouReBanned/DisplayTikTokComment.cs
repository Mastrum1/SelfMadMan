using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

public class DisplayTikTokComment : MonoBehaviour
{
    public Image ActualBackground { get => _mActualBackground; set => _mActualBackground = value; }
    public Girls Girls { get => _mSelectedProfile; set => _mSelectedProfile = value; }
    public TikTokCommentData SelectedData { get => _mSelectedData; set => _mSelectedData = value; }
    public bool IsEnable { get => _mIsEnable; set => _mIsEnable = value; }

    [Header("Comment data")]
    [SerializeField] List<TikTokCommentData> _mData;
    private TikTokCommentData _mSelectedData;
    
    [Header("Profile SO")]
    [SerializeField] List<Girls> _mProfileSO;
    private Girls _mSelectedProfile;

    [Header("Profile Picture Image")]
    [SerializeField] List<Sprite> _mBackgroudPicture;
    [SerializeField] Image _mActualProfilPicture;
    [SerializeField] Image _mActualBackground;

    [Header("Profile Text")]
    [SerializeField] TMP_Text _mProfileName;
    [SerializeField] TMP_Text _mComment;

    [Header("Divers")]
    [SerializeField] Sprite _mBaseState;
    [SerializeField] Sprite _mDeleteState;

    private bool _mIsEnable;
    private bool _mIsDeleted;

    public event Action<bool, GameObject> DeleteComment;


    void Start()
    {
        //ChangeProfile();
    }

    public void OnDeleteComment()
    {
        Debug.Log("Aled");
        DeleteComment?.Invoke(_mSelectedData.IsGood, gameObject);      
        Disable();
    }

    [ContextMenu("ChangeProfile")]
    public void ChangeProfile()
    {
        //Take a random profile and add the name et the picture + random background
        _mSelectedProfile = _mProfileSO[UnityEngine.Random.Range(0, _mProfileSO.Count)];
        _mActualProfilPicture.sprite = _mSelectedProfile.ProfilePicture;
        int index = UnityEngine.Random.Range(0, _mBackgroudPicture.Count);
        _mActualBackground.sprite = _mBackgroudPicture[index];
        _mProfileName.text = _mSelectedProfile.Pseudo;
        //Select a random comment
        _mSelectedData = _mData[UnityEngine.Random.Range(0, _mData.Count)];
        _mComment.text = _mSelectedData.CommentContent;
    }

    public void UpdateComment()
    {
        _mComment.text = _mSelectedData.CommentContent;  
        _mActualProfilPicture.sprite = _mSelectedProfile.ProfilePicture;
        _mProfileName.text = _mSelectedProfile.Pseudo;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public bool GetIsGood()
    {
        return _mSelectedData.IsGood;
    }
}
