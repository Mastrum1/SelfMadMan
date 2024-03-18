using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    
    [SerializeField] private List<Quests> _questsList;
    private readonly List<Quests> _activeQuestsList = new List<Quests>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
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
        if (!CheckForActiveQuest())
        {
            SetNewActiveQuest();
        }
    }

    bool CheckForActiveQuest()
    {

        foreach (var quest in _questsList)
        {
            if (quest.status == Quests.State.Active)
            {
                _activeQuestsList.Add(quest);
            }
        }
        
        foreach (var quest in _activeQuestsList)
        {
            if (quest.status == Quests.State.Inactive)
            {
                _activeQuestsList.Remove(quest);
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
            var randomNum = Random.Range(0, _questsList.Count);
            if (_questsList[randomNum].disponibility == Quests.QuestDispo.Unlocked && _questsList[randomNum].status == Quests.State.Inactive)
            {
                _questsList[randomNum].status = Quests.State.Active;
                _activeQuestsList.Add(_questsList[randomNum]);
            }
        }
    }

    void ChangeQuest(Quests quest)
    {
        quest.status = Quests.State.Inactive;
        SetNewActiveQuest();
    }
    
    void UnlockQuest(int time)
    {
        foreach (var quest in _questsList)
        {
            if (quest.time == time)
            {
                quest.disponibility = Quests.QuestDispo.Unlocked;
            }
        }
    }
    
    void LockQuest(int time)
    {
        foreach (var quest in _questsList)
        {
            if (quest.time == time)
            {
                quest.disponibility = Quests.QuestDispo.Locked;
            }
        }
    }
}
