using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public event Action<int> OnQuestComplete; 
    public enum State
    {
        Inactive, Complete, Active
    }
    [Serializable]
    public class Quest
    {
        [SerializeField] private Quests _questSO;
        public Quests QuestSO { get => _questSO; set => _questSO = value; }

        [SerializeField] private State _questState;
        public State QuestState { get => _questState; set => _questState = value; }
        
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
            _questState = State.Inactive;
            if (_questSO)
                _questDispo = _questSO.disponibility;
        }
    }
    
    [SerializeField] private List<Quest> _questsList;
    private readonly List<Quest> _activeQuestsList = new List<Quest>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

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
            } while (_activeQuestsList.Count < 3);
        }
    }
    
    void Update()
    {
        CheckQuestCompletion();
    }

    bool CheckForActiveQuest()
    {
        if (_activeQuestsList.Count < 3)
        {
            foreach (var item in _questsList)
            {
                if (item.QuestState == State.Active)
                {
                    _activeQuestsList.Add(item);
                }
            }
        }
        
        foreach (var item in _activeQuestsList)
        {
            if (item.QuestState == State.Inactive)
            {
                _activeQuestsList.Remove(item);
            }
        }

        if (_activeQuestsList.Count is 3)
        {
            return true;
        }

        return false;
    }

    void SetNewActiveQuest()
    {
        if (_activeQuestsList.Count < 3)
        {
            var randomQuest = Random.Range(0, _questsList.Count);
            var randomDifficulty = Random.Range(0, 2);
            if (_questsList[randomQuest].QuestDispo == Quests.QuestBaseDispo.Unlocked && _questsList[randomQuest].QuestState == State.Inactive)
            {
                _questsList[randomQuest].QuestState = State.Active;
                Quest temp = _questsList[randomQuest];
                temp.Difficulty = temp.QuestSO.difficulties[randomDifficulty];
                temp.MaxAmount = temp.Difficulty.amount;
                temp.CurrentAmount = 0;
                _activeQuestsList.Add(temp);
            }
        }
    }

    void CheckQuestCompletion()
    {
        foreach (var item in _activeQuestsList)
        {
            if (item.CurrentAmount >= item.MaxAmount)
            {
                item.QuestState = State.Complete;
            }
        }
    }

    void OnQuestFinish(Quest quest)
    {
        quest.QuestState = State.Inactive;
        OnQuestComplete?.Invoke(quest.Difficulty.reward);
        
        if (!CheckForActiveQuest())
        {
            SetNewActiveQuest();
        }
    }
    void ChangeQuest(Quest quest)
    {
        quest.QuestState = State.Inactive;
        if (!CheckForActiveQuest())
        {
            SetNewActiveQuest();
        }
    }
    
    void UnlockQuest(int time)
    {
        foreach (var quest in _questsList)
        {
            if (quest.QuestSO.time == time)
            {
                quest.QuestDispo = Quests.QuestBaseDispo.Unlocked;
            }
        }
    }
    
    void LockQuest(int time)
    {
        foreach (var quest in _questsList)
        {
            if (quest.QuestSO.time == time)
            {
                quest.QuestDispo = Quests.QuestBaseDispo.Locked;
            }
        }
    }
}
