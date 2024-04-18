using System;
using UnityEngine;
using UnityEngine.UI;

public class QuestAndAchievement : MonoBehaviour
{
    [SerializeField] private GameObject _mQuestView;
    [SerializeField] private GameObject _mTrophyView;

    [Header("ChangeButtons")]
    [SerializeField] private Image _mQuestButton;
    [SerializeField] private Image _mTrophyButton;

    Color transparent = new Color(1f, 1f, 1f, 0.5f);
    Color full = new Color(1f, 1f, 1f, 1f);

    private void OnEnable()
    {
        if (!_mQuestView.activeSelf)
        {
            _mQuestView.SetActive(true);
            _mQuestButton.color = full;
        }

        if (_mTrophyView.activeSelf)
        {
            _mTrophyView.SetActive(false);
            _mTrophyButton.color = transparent;
        }
    }

    public void EnableQuests()
    {
        _mTrophyView.SetActive(false);
        _mTrophyButton.color = transparent;
        _mQuestView.SetActive(true);
        _mQuestButton.color = full;
    }

    public void EnableTrophies()
    {
        _mQuestView.SetActive(false);
        _mQuestButton.color = transparent;
        _mTrophyView.SetActive(true);
        _mTrophyButton.color = full;
    }

    private void OnDisable()
    {
        _mQuestView.SetActive(false);
        _mTrophyView.SetActive(false);
    }
}
