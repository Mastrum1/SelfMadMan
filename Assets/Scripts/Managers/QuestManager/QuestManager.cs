using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public event Action<Quest> OnQuestComplete;

    public event Action<int> OnReward;

    public event Action<Quest> OnAddActiveQuest;

    public event Action<int, Quest> OnUpdateQuestUI;

    public event Action<Quest> OnRemoveActiveQuest;

    public event Action<int> OnUnlockQuest;

    public event Action<int> OnLockQuest;

    public event Action<Quest> OnQuestFinished;

    public enum CompletionState
    {
        NotSelected, Complete, Selected
    }

    [Serializable]
    public class Quest
    {
        [SerializeField] private Quests _questSO;
        public Quests QuestSO { get => _questSO; set => _questSO = value; }

        [SerializeField] private CompletionState _questCompletionState;
        public CompletionState QuestCompletionState { get => _questCompletionState; set => _questCompletionState = value; }

        [SerializeField] private Quests.QuestBaseDispo _questDispo;
        public Quests.QuestBaseDispo QuestDispo { get => _questDispo; set => _questDispo = value; }

        private Quests.Difficulty _difficulty;
        public Quests.Difficulty Difficulty { get => _difficulty; set => _difficulty = value; }

        private int _maxAmount;
        public int MaxAmount { get => _maxAmount; set => _maxAmount = value; }

        private int _currentAmount;
        public int CurrentAmount { get => _currentAmount; set => _currentAmount = value; }

        public Quest(Quests questSO, CompletionState questCompletionState, Quests.QuestBaseDispo questDispo, Quests.Difficulty difficulty, int maxAmount, int currentAmount)
        {
            QuestSO = questSO;
            QuestCompletionState = questCompletionState;
            QuestDispo = questDispo;
            Difficulty = difficulty;
            MaxAmount = maxAmount;
            CurrentAmount = currentAmount;
        }
    }

    [SerializeField] private List<Quest> _questsList;
    private List<Quest> _selectedQuestsList = new List<Quest>();
    public List<Quest> SelectedQuests => _selectedQuestsList;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void LoadQuests(List<Quest> quests, List<Quest> activeQuests)
    {
        _questsList = quests;
        _selectedQuestsList = activeQuests;

        while (activeQuests.Count < 3)
        {
            SetNewActiveQuest();
        }
    }

    private void RemoveActiveQuest(Quest quest)
    {
        _selectedQuestsList.Remove(quest);
        OnRemoveActiveQuest?.Invoke(quest);
    }

    private Quest SetNewActiveQuest()
    {
        if (_selectedQuestsList.Count >= 3) return null;

        var randomQuest = Random.Range(0, _questsList.Count);
        var randomDifficulty = Random.Range(0, 3);

        if (_questsList[randomQuest].QuestDispo != Quests.QuestBaseDispo.Unlocked ||
            _questsList[randomQuest].QuestCompletionState != CompletionState.NotSelected) return null;

        _questsList[randomQuest].QuestCompletionState = CompletionState.Selected;
        var temp = _questsList[randomQuest];
        temp.Difficulty = temp.QuestSO.difficulties[randomDifficulty];
        temp.MaxAmount = temp.Difficulty.amount;
        temp.CurrentAmount = 0;
        _selectedQuestsList.Add(temp);
        OnAddActiveQuest?.Invoke(temp);
        return temp;
    }

    public void CheckQuestCompletion(Quest quest)
    {
        if (quest.CurrentAmount < quest.MaxAmount) return;

        quest.QuestCompletionState = CompletionState.Complete;
        OnQuestComplete?.Invoke(quest);
    }

    public void OnQuestFinish(Quest quest, int container)
    {
        Debug.Log("Quest ,� " + quest.QuestSO.questDescription + " finished");
        quest.QuestCompletionState = CompletionState.NotSelected;
        OnReward?.Invoke(quest.Difficulty.reward);
        OnQuestFinished?.Invoke(quest);
        RemoveActiveQuest(quest);
        while (_selectedQuestsList.Count < 3)
        {
            OnUpdateQuestUI?.Invoke(container, SetNewActiveQuest());
        }
    }
    public void OnChangeQuest(Quest quest, int container)
    {
        quest.QuestCompletionState = CompletionState.NotSelected;
        RemoveActiveQuest(quest);
        while (_selectedQuestsList.Count < 3)
        {
            OnUpdateQuestUI?.Invoke(container, SetNewActiveQuest());
        }
    }

    public void UnlockQuest(string sceneName)
    {
        foreach (var quest in _questsList.Where(quest => quest.QuestSO.scene.SceneName == sceneName).ToArray())
        {
            quest.QuestDispo = Quests.QuestBaseDispo.Unlocked;
            OnUnlockQuest?.Invoke(quest.QuestSO.ID);
        }
    }

    public void LockQuest(string sceneName)
    {
        foreach (var quest in _questsList.Where(quest => quest.QuestSO.scene.SceneName == sceneName).ToArray())
        {
            quest.QuestDispo = Quests.QuestBaseDispo.Locked;
            OnLockQuest?.Invoke(quest.QuestSO.ID);
        }
    }
}
