using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public event Action<int> OnQuestComplete; 
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
        public int MaxAmount {get => _maxAmount; set => _maxAmount = value;}
        
        private int _currentAmount;
        public int CurrentAmount {get => _currentAmount; set => _currentAmount = value;}

        public Quest()
        {
            _questCompletionState = CompletionState.NotSelected;
            if (_questSO)
                _questDispo = _questSO.disponibility;
        }
    }
    
    [SerializeField] private List<Quest> _questsList;
    private readonly List<Quest> _selectedQuestsList = new List<Quest>();
    public List<Quest> SelectedQuests => _selectedQuestsList;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        foreach (var item in _questsList)
        {
            item.QuestDispo = item.QuestSO.disponibility;
        }
    }

    void Start()
    {
        if (!CheckForActiveQuest())
        {
            do
            {
                SetNewActiveQuest();
            } while (SelectedQuests.Count < 3);
        }
        
        
    }

    bool CheckForActiveQuest()
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
        }

        return SelectedQuests.Count is 3;
    }

    void SetNewActiveQuest()
    {
        if (SelectedQuests.Count >= 3) return;
        
        var randomQuest = Random.Range(0, _questsList.Count);
        var randomDifficulty = Random.Range(0, 3);
        
        if (_questsList[randomQuest].QuestDispo != Quests.QuestBaseDispo.Unlocked ||
            _questsList[randomQuest].QuestCompletionState != CompletionState.NotSelected) return;
        
        _questsList[randomQuest].QuestCompletionState = CompletionState.Selected;
        Quest temp = _questsList[randomQuest];
        temp.Difficulty = temp.QuestSO.difficulties[randomDifficulty];
        temp.MaxAmount = temp.Difficulty.amount;
        temp.CurrentAmount = 0;
        SelectedQuests.Add(temp);
    }
    
    public void CheckQuestCompletion()
    {
        foreach (var quest in SelectedQuests.Where(item => item.CurrentAmount >= item.MaxAmount))
        {
            quest.QuestCompletionState = CompletionState.Complete;
        }
    }

    void OnQuestFinish(Quest quest)
    {
        quest.QuestCompletionState = CompletionState.NotSelected;
        OnQuestComplete?.Invoke(quest.Difficulty.reward);
        
        if (!CheckForActiveQuest())
        {
            SetNewActiveQuest();
        }
    }
    void ChangeQuest(Quest quest)
    {
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
            quest.QuestDispo = Quests.QuestBaseDispo.Unlocked;
        }
    }
    
    void LockQuest(int time)
    {
        foreach (var quest in _questsList.Where(quest => quest.QuestSO.time == time))
        {
            quest.QuestDispo = Quests.QuestBaseDispo.Locked;
        }
    }
}
