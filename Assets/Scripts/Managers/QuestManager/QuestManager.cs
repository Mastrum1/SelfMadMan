using System;
using System.Collections.Generic;
using UnityEngine;
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
        public Quests questSO;
        public State questState;
        public Quests.QuestBaseDispo questDispo;
        [NonSerialized] public Quests.Difficulty difficulty;
        [NonSerialized] public int maxAmount;
        [NonSerialized] public int currentAmount;

        public Quest()
        {
            questState = State.Inactive;
            if (questSO)
                questDispo = questSO.disponibility;
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
            item.questDispo = item.questSO.disponibility;
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
                if (item.questState == State.Active)
                {
                    _activeQuestsList.Add(item);
                }
            }
        }
        
        foreach (var item in _activeQuestsList)
        {
            if (item.questState == State.Inactive)
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
            if (_questsList[randomQuest].questDispo == Quests.QuestBaseDispo.Unlocked && _questsList[randomQuest].questState == State.Inactive)
            {
                _questsList[randomQuest].questState = State.Active;
                Quest temp = _questsList[randomQuest];
                temp.difficulty = temp.questSO.difficulties[randomDifficulty];
                temp.maxAmount = temp.difficulty.amount;
                temp.currentAmount = 0;
                _activeQuestsList.Add(temp);
            }
        }
    }

    void CheckQuestCompletion()
    {
        foreach (var item in _activeQuestsList)
        {
            if (item.currentAmount >= item.maxAmount)
            {
                item.questState = State.Complete;
            }
        }
    }

    void OnQuestFinish(Quest quest)
    {
        quest.questState = State.Inactive;
        OnQuestComplete?.Invoke(quest.difficulty.reward);
        
        if (!CheckForActiveQuest())
        {
            SetNewActiveQuest();
        }
    }
    void ChangeQuest(Quest quest)
    {
        quest.questState = State.Inactive;
        if (!CheckForActiveQuest())
        {
            SetNewActiveQuest();
        }
    }
    
    void UnlockQuest(int time)
    {
        foreach (var quest in _questsList)
        {
            if (quest.questSO.time == time)
            {
                quest.questDispo = Quests.QuestBaseDispo.Unlocked;
            }
        }
    }
    
    void LockQuest(int time)
    {
        foreach (var quest in _questsList)
        {
            if (quest.questSO.time == time)
            {
                quest.questDispo = Quests.QuestBaseDispo.Locked;
            }
        }
    }
}
