using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public int Level = 1;

    public int Xp = 100;

    public int Money = 100;

    public List<int> Inventory = new List<int>();

    public List<int> ItemLocked = new List<int>();

    public List<int> ActiveQuests = new List<int>();

    public List<int> FinishedQuests = new List<int>();

    public List<int> UnlockedQuest = new List<int>();

    public List<int> LockedQuest = new List<int>();

    public Dictionary<int, bool> _mMinigames = new Dictionary<int, bool>();

    public void SaveData(Player player)
    {
        Level = player.Level;
        Xp = player.Xp;
        Money = player.Money;
        _mMinigames = player.Minigames;

        foreach (var item in player.Inventory)
        {
            Inventory.Add(item.ID);
        }
        foreach (var item in player.ItemLocked)
        {
            ItemLocked.Add(item.ID);
        }
        foreach (var item in player.ActiveQuests)
        {
            ActiveQuests.Add(item.ID);
        }
        foreach (var item in player.FinishedQuests)
        {
            FinishedQuests.Add(item.ID);
        }
        foreach (var item in player.QuestUnlocked)
        {
            UnlockedQuest.Add(item.ID);

        }
        foreach (var item in player.QuestLocked)
        {
            LockedQuest.Add(item.ID);

        }
    }


}
