using System;
using UnityEngine;

public class QuestAndAchievement : MonoBehaviour
{
    [SerializeField] private GameObject _mQuestView;
    [SerializeField] private GameObject _mTrophyView;

    private void OnEnable()
    {
        if (!_mQuestView.activeSelf)
        {
            _mQuestView.SetActive(true);
        }

        if (_mTrophyView.activeSelf)
        {
            _mTrophyView.SetActive(false);
        }
    }

    public void EnableQuests()
    {
        _mTrophyView.SetActive(false);
        _mQuestView.SetActive(true);
    }

    public void EnableTrophies()
    {
        _mQuestView.SetActive(false);
        _mTrophyView.SetActive(true);
    }

    private void OnDisable()
    {
        _mQuestView.SetActive(false);
        _mTrophyView.SetActive(false);
    }
}
