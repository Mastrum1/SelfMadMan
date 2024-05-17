using System.Collections;
using System.Collections.Generic;
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

    private void LoadQuestContainers()
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
    
    public IEnumerator CheckForCompletedQuests()
    {
        yield return new WaitForSeconds(2f);
        for (var i = 0; i < _quests.Count; i++)
        {
            if (_quests[i].SelectedQuest.QuestCompletionState != QuestManager.CompletionState.Complete) continue;
            
            _mQuestManager.OnQuestFinish(_quests[i].SelectedQuest, i);
        }
    }
    public void CompleteAllBars()
    {
        foreach (var item in _quests)
        {
            item.StartCompletion();  
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
