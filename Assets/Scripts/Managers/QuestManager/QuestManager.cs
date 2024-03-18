using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public enum State
    {
        Inactive, Active
    }
    [Serializable]
    public class Quest
    {
        public Quests questSO;
        [NonSerialized] public State questState;
        [NonSerialized] public Quests.QuestBaseDispo questDispo;
        [NonSerialized] public int difficulty;
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
            Debug.Log(_activeQuestsList.Count);
        }
    }
    
    void Update()
    {
        if (!CheckForActiveQuest())
        {
            SetNewActiveQuest();
        }
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
                    Debug.Log("Added");
                }
            }
        }
        
        foreach (var item in _activeQuestsList)
        {
            if (item.questState == State.Inactive)
            {
                _activeQuestsList.Remove(item);
                Debug.Log("Removed");
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
                temp.difficulty = randomDifficulty;
                temp.currentAmount = 0;
                _activeQuestsList.Add(temp);
            }
        }
    }

    void ChangeQuest(Quest quest)
    {
        quest.questState = State.Inactive;
        SetNewActiveQuest();
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
