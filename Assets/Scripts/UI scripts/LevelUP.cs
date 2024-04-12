using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUP : MonoBehaviour
{
    [SerializeField] private LevelSO[] _mLevelSO;
    [SerializeField] private Money _mMoney;

    [Header("UI Elements")]
    [SerializeField] private TMPro.TextMeshProUGUI _mLevelText;
    [SerializeField] private TMPro.TextMeshProUGUI _mTitleText;
    [SerializeField] private TMPro.TextMeshProUGUI _mRewardText;
    
    public void CheckLevelUP(int level)
    {
        _mLevelText.text = _mLevelSO[level].Level.ToString();
        _mTitleText.text = _mLevelSO[level].Title;
        _mRewardText.text = _mLevelSO[level].Reward.ToString();
    }

    public void OnLevelUP()
    {
        GameManager.instance.Player.LvlUp();
        CheckLevelUP(GameManager.instance.Player.Level);
        _mMoney.AddMoney(_mLevelSO[GameManager.instance.Player.Level].Reward);
    }
}
