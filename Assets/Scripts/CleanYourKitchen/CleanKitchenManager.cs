using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class CleanKitchenManager : MiniGameManager
{
    private QuestManager _questManager;
    private List<QuestManager.Quest> _quests;
    private void Start()
    {
        _questManager = QuestManager.instance;
        CheckForActiveQuest(mySceneManager.instance.MinigameScene);
    }
    
    void CheckForActiveQuest(string scene)
    {
        foreach (var quest in _questManager.ActiveQuests.Where(quest => quest.QuestSO.scene == scene))
        {
            _quests.Add(quest);
        }
    }
    
    
}
