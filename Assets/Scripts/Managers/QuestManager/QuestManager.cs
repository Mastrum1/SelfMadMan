using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public event Action<Quest> OnQuestComplete;

    public event Action<int> OnReward;

    public event Action<Quest> OnAddActiveQuest;

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
        if (instance == null)
            instance = this;
        else if (instance != this)
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

    private bool CheckForActiveQuest()
    {
        if (SelectedQuests.Count < 3)
        {
            foreach (var item in _questsList.Where(item => item.QuestCompletionState == CompletionState.Selected))
            {
                SelectedQuests.Add(item);
            }
        }

        foreach (var item in SelectedQuests.Where(item => item.QuestCompletionState == CompletionState.NotSelected))
        {
            SelectedQuests.Remove(item);
            OnRemoveActiveQuest?.Invoke(item);

        }

        return SelectedQuests.Count is 3;
    }

    private void SetNewActiveQuest()
    {
        if (SelectedQuests.Count >= 3) return;

        var randomQuest = Random.Range(0, _questsList.Count);
        var randomDifficulty = Random.Range(0, 3);

        if (_questsList[randomQuest].QuestDispo != Quests.QuestBaseDispo.Unlocked ||
            _questsList[randomQuest].QuestCompletionState != CompletionState.NotSelected) return;

        _questsList[randomQuest].QuestCompletionState = CompletionState.Selected;
        var temp = _questsList[randomQuest];
        temp.Difficulty = temp.QuestSO.difficulties[randomDifficulty];
        temp.MaxAmount = temp.Difficulty.amount;
        temp.CurrentAmount = 3;
        SelectedQuests.Add(temp);

        OnAddActiveQuest?.Invoke(temp);
    }

    public void CheckQuestCompletion()
    {
        foreach (var quest in SelectedQuests.Where(item => item.CurrentAmount >= item.MaxAmount))
        {
            OnQuestComplete?.Invoke(quest);
            quest.QuestCompletionState = CompletionState.Complete;
        }
    }

    void OnQuestFinish(Quest quest)
    {
        quest.QuestCompletionState = CompletionState.NotSelected;
        OnQuestFinished?.Invoke(quest);
        OnReward?.Invoke(quest.Difficulty.reward);
        if (!CheckForActiveQuest())
        {
            SetNewActiveQuest();
        }
    }
    void ChangeQuest(Quest quest)
    {
        OnRemoveActiveQuest?.Invoke(quest);
        quest.QuestCompletionState = CompletionState.NotSelected;
        if (!CheckForActiveQuest())
        {
            SetNewActiveQuest();
        }
    }

    void UnlockQuest(int time)
    {
        foreach (var quest in _questsList.Where(quest => quest.QuestSO.time == time))
        {
            OnUnlockQuest?.Invoke(quest.QuestSO.ID);
            quest.QuestDispo = Quests.QuestBaseDispo.Unlocked;
        }
    }

    void LockQuest(int time)
    {
        foreach (var quest in _questsList.Where(quest => quest.QuestSO.time == time))
        {
            OnLockQuest?.Invoke(quest.QuestSO.ID);
            quest.QuestDispo = Quests.QuestBaseDispo.Locked;
        }
    }
}
