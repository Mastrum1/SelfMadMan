using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUP : MonoBehaviour
{
    [SerializeField] private LevelSO[] _mLevelSO;

    [Header("UI Elements")]
    [SerializeField] private TMPro.TextMeshProUGUI _mLevelText;
    [SerializeField] private TMPro.TextMeshProUGUI _mTitleText;
    [SerializeField] private TMPro.TextMeshProUGUI _mRewardText;

    private void OnEnable()
    {
        OnLevelUP();
    }

    public void OnLevelUP()
    {
        CheckLevelUP(GameManager.instance.Player.Level);
        MoneyManager.Instance.AddMoney(_mLevelSO[GameManager.instance.Player.Level].Reward);
    }

    public void CheckLevelUP(int level)
    {
        _mLevelText.text = _mLevelSO[level].Level.ToString();
        _mTitleText.text = _mLevelSO[level].Title;
        _mRewardText.text = _mLevelSO[level].Reward.ToString();
    }
}
