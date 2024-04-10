using UnityEngine;

public class QuestAndAchievement : MonoBehaviour
{
    [SerializeField] private GameObject _mQuestView;

    public void EnableQuests(bool isEnabled)
    {
        _mQuestView.SetActive(isEnabled);
    }
}
