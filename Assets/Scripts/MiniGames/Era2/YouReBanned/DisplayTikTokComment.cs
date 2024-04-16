using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;

public class DisplayTikTokComment : MonoBehaviour
{
    //Search for a way to add more than 1 color to textmeshpro
    [SerializeField] TikTokCommentData _mData;
    

    [Header("Profile SO")]
    [SerializeField] List<Girls> _mProfileSO;

    [Header("Profile Picture Image")]
    [SerializeField] List<Image> _mBackgroudPicture;
    [SerializeField] Image _actualProfilPicture;
    [SerializeField] Image _actualBackground;

    [Header("Profile Text")]
    [SerializeField] TMP_Text _mProfileName;
    [SerializeField] TMP_Text _mComment;

    [Header("Divers")]
    [SerializeField] Sprite _mBaseState;
    [SerializeField] Sprite _mDeleteState;

    private Girls _mSelectedProfile;

    private bool _mIsEnable;
    private bool _mIsDeleted;

    public event Action<bool, GameObject> DeleteComment;
    public event Action<bool, GameObject> ExitScreen;

    void Start()
    {
        
    }

    public void OnDeleteComment()
    {
        if (_mIsEnable) {
            _mIsDeleted = true;
           // _mDeletePicture.sprite = _mDeleteState;
            Disable();
            DeleteComment?.Invoke(_mData.IsGood, this.transform.gameObject);
        }
    }

    public void ChangeProfile()
    {
        _mSelectedProfile = _mProfileSO[UnityEngine.Random.Range(0, _mProfileSO.Count)];
        _actualProfilPicture.sprite = _mSelectedProfile.ProfilePicture;
        int index = UnityEngine.Random.Range(0, _mBackgroudPicture.Count);
        _actualBackground.sprite = _mBackgroudPicture[index].sprite;

    }

    public void Disable()
    {
        _mIsEnable = false;
    }

    public bool GetIsGood()
    {
        return _mData.IsGood;
    }
}
