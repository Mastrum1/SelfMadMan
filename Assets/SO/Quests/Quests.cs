using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Quests", menuName = "SO/NewQuest", order = 1)]
public class Quests : ScriptableObject
{
    public enum QuestBaseDispo
    {
        Locked,
        Unlocked
    }
    
    
    public enum Difficulties
    {
        Easy,
        Normal,
        Hard
    }
    
    [Serializable]
    public class Difficulty
    {
        public Difficulties difficulty;
        public  int amount;
        public int reward;
    }
    
    public string questName;
    public int time;
    public string questDescription;
    public List<Difficulty> difficulties;

    public QuestBaseDispo disponibility;
  
}
