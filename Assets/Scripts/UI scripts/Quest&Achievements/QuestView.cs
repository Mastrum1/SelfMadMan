using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestView : MonoBehaviour
{
    private QuestManager _mQuestManager;

    [SerializeField] private List<QuestContainer> _quests;

    private bool _questAlreadyLoaded;

    private void OnEnable()
    {
        _mQuestManager = QuestManager.Instance;
        _mQuestManager.OnUpdateQuestUI += UpdateContainer;
        
        LoadQuestContainers();
    }

    public void LoadQuestContainers()
    {
        for (var i = 0; i < _quests.Count; i++)
        {
            if (!_questAlreadyLoaded)
            {
                _quests[i].SelectedQuest = _mQuestManager.SelectedQuests[i];
            }
            _quests[i].UpdateQuestUI();
        }

        if (!_questAlreadyLoaded)
        {
            _questAlreadyLoaded = true;
        }
    }
    
    private void UpdateContainer(int container, QuestManager.Quest quest)
    {
        _quests[container].SelectedQuest = quest;
        _quests[container].UpdateQuestUI();
    }

    public void IsInQuestMenu()
    {
        foreach (var quest in _quests)
        {
            quest.GetComponent<QuestContainer>().IsInQuestMenu = true;
        }
    }

    private void OnDisable()
    {
        foreach (var quest in _quests)
        {
            quest.GetComponent<QuestContainer>().ResetFillBar();
        }
    }
}
