using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestView : MonoBehaviour
{
    private QuestManager _mQuestManager;

    [SerializeField] private List<QuestContainer> _quests;

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
            _quests[i].SelectedQuest = _mQuestManager.SelectedQuests[i];
            _quests[i].UpdateQuestUI();
        }
    }
    
    private void UpdateContainer(int container, QuestManager.Quest quest)
    {
        if (quest == null) return;
        if (_quests[container] == null)
        {
            return;
        }
        _quests[container].SelectedQuest = quest;
        _quests[container].UpdateQuestUI();
    }
    
    public void CheckForCompletedQuests()
    {
        for (var i = 0; i < _quests.Count; i++)
        {
            if (_quests[i].SelectedQuest.QuestCompletionState != QuestManager.CompletionState.Complete) continue;
            
            _mQuestManager.OnQuestFinish(_quests[i].SelectedQuest, i);
        }
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
