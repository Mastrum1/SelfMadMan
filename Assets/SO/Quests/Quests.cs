using System;
using UnityEngine;
using UnityEngine.Serialization;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Quests", menuName = "SO/NewQuest", order = 1)]
public class Quests : ScriptableObject
{
    public enum QuestDispo
    {
        Locked,
        Unlocked
    }
    
    public enum State
    {
        Inactive,
        Active
    }
    
    public enum Difficulties
    {
        Easy,
        Normal,
        Hard
    }
    
    [System.Serializable]
    public class Difficulty
    {
        public Difficulties difficulty;
        public  int amount;
    }
    
    public string questName;
    public int time;
    public string questDescription;
    //public int reward;

    public QuestDispo disponibility;
    [NonSerialized] public State status;
}
