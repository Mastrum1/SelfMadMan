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
    [SerializeField] TMP_Text _mComment;

    [Header("Profile SO")]
    [SerializeField] List<Girls> _mProfileSO;

    [Header("Profile Picture Image")]
    [SerializeField] List<Image> _mProfilPicture;
    [SerializeField] List<Image> _mDeletePicture;

    [SerializeField] Sprite _mBaseState;
    [SerializeField] Sprite _mDeleteState;
    private bool _mIsEnable;
    private bool _mIsDeleted;
    public event Action<bool, GameObject> DeleteComment;
    public event Action<bool, GameObject> ExitScreen;

    void Start()
    {
        _mIsEnable = true;
        _mIsDeleted = false;
        //Spawn texts + pictures
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("GameInteractable")) {
            Disable();
            ExitScreen?.Invoke(_mData.IsGood, this.transform.gameObject);
            ResetComment();
        }
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

    public void ResetComment()
    {
        //_mDeletePicture.sprite = _mBaseState;
        _mIsEnable = true;
        _mIsDeleted = false;
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
