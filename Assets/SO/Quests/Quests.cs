using System;
using UnityEngine;
using UnityEngine.Serialization;

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

    public string questName;
    public int time;
    public string questDescription;
    public int easy;
    public int normal;
    public int hard;
    public string questDescription2;
    //public int reward;

    public QuestDispo disponibility;
    [NonSerialized] public State status;
}
