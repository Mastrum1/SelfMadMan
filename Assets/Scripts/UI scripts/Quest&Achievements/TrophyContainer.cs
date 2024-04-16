using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrophyContainer : MonoBehaviour
{
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

    private void OnEnable()
    {
        _mStartPos = _mTrophyProgression.transform.position.x;
    }
    
    public void DisableRewardButton()
    {
        _mRewardButton.SetActive(false);
    }
}
