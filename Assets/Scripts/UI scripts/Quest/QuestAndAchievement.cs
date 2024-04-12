using System;
using UnityEngine;

public class QuestAndAchievement : MonoBehaviour
{
    [SerializeField] private GameObject _mQuestView;

    private void OnEnable()
    {
        if (!_mQuestView.activeSelf)
        {
            _mQuestView.SetActive(true);
        }
    }

    public void EnableQuests(bool isEnabled)
    {
        _mQuestView.SetActive(isEnabled);
    }

    private void OnDisable()
    {
        _mQuestView.SetActive(false);
    }
}
