using System;
using UnityEngine;

public class QuestAndAchievement : MonoBehaviour
{
    [SerializeField] private GameObject _mQuestView;
    [SerializeField] private GameObject _mTrophyView;

    private void OnEnable()
    {
        EnableQuests();
    }

    public void EnableQuests()
    {
        _mTrophyView.SetActive(false);
        _mQuestView.SetActive(true);
        _mQuestView.GetComponent<QuestView>().IsInQuestMenu();
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
