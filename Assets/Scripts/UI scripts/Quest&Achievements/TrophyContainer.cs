using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrophyContainer : MonoBehaviour
{
    public TrophyManager.Trophy Trophy { get => _trophy; set => _trophy = value; }
    private TrophyManager.Trophy _trophy;
    
    public TMP_Text TrophyDescription { get => _mTrophyDescription; set => _mTrophyDescription = value; }
    [SerializeField] private TMP_Text _mTrophyDescription;

    public GameObject TrophyProgression => _mTrophyProgression;
    [SerializeField] private GameObject _mTrophyProgression;

    public GameObject TrophyIcon => _mTrophyIcon;
    [SerializeField] private GameObject _mTrophyIcon;
    
    public GameObject TrophyLock => _mTrophyLock;
    [SerializeField] private GameObject _mTrophyLock;
    
    public GameObject RewardButton => _mRewardButton;
    [SerializeField] private GameObject _mRewardButton;
    
    public GameObject CompleteTag => _mCompleteTag;
    [SerializeField] private GameObject _mCompleteTag;
    
    public float StartPosX => _mStartPos;
    private float _mStartPos;

    private void Awake()
    {
        _mStartPos = _mTrophyProgression.transform.position.x;
    }
    
    public void DisableRewardButton()
    {
        _mRewardButton.SetActive(false);
        TrophyManager.Instance.ClaimReward(_trophy);
    }

    private void OnDestroy()
    {
        _mTrophyProgression.transform.position = new Vector3(_mStartPos, _mTrophyProgression.transform.position.y);
    }
}
