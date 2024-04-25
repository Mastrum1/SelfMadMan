using System;
using UnityEngine;

public class QuestAndAchievement : MonoBehaviour
{
    [SerializeField] private QuestView _mQuestView;
    [SerializeField] private GameObject _mTrophyView;

    private void OnEnable()
    {
        EnableQuests();
    }

    public void EnableQuests()
    {
        _mTrophyView.SetActive(false);
        _mQuestView.IsInQuestMenu();
        _mQuestView.gameObject.SetActive(true);
        
    }

    public void EnableTrophies()
    {
        _mQuestView.gameObject.SetActive(false);
        _mTrophyView.SetActive(true);
    }

    private void OnDisable()
    {
        _mQuestView.gameObject.SetActive(false);
        _mTrophyView.SetActive(false);
    }
}
