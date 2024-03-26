using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Platform.Android;
using UnityEngine;
using UnityEngine.Pool;

[System.Serializable]
public class Player : MonoBehaviour
{
    private PlayerData playerData = new PlayerData();

    private IDataService DataService = new JsonData();

    public int Level { get => _mLevel; private set => _mLevel = value; }

    [SerializeField] private int _mLevel;

    public int Xp { get => _mXp; private set => _mXp = value; }

    [SerializeField] private int _mXp;

    public int Money { get => _mMoney; private set => _mMoney = value; }

    [SerializeField] private int _mMoney;

    public Items[] Inventory { get => _mInventory; private set => _mInventory = value; }

    [SerializeField] private Items[] _mInventory;

    public Items[] ItemLocked { get => _mItemLocked; private set => _mItemLocked = value; }

    [SerializeField] private Items[] _mItemLocked;
    public Quests[] ActiveQuests { get => _mActiveQuests; private set => _mActiveQuests = value; }

    [SerializeField] private Quests[] _mActiveQuests;

    public Quests[] FinishedQuests { get => _mFinishedQuest; private set => _mFinishedQuest = value; }

    [SerializeField] private Quests[] _mFinishedQuest;

    public Quests[] QuestLocked { get => _mQuestLocked; private set => _mQuestLocked = value; }

    [SerializeField] private Quests[] _mQuestLocked;

    public Quests[] QuestUnlocked { get => _mQuestUnlocked; private set => _mQuestUnlocked = value; }

    [SerializeField] private Quests[] _mQuestUnlocked;

    public Dictionary<int, bool> Minigames { get => _mMinigames; private set => _mMinigames = value; }

    private Dictionary<int, bool> _mMinigames = new Dictionary<int, bool>();


    public void SaveJson()
    {
        _mMinigames.Add(0, true);
        playerData.SaveData(this);
        if (DataService.SaveData("/player-stats.json", playerData, true))
        {

        }
        else
        {
            Debug.LogError("Could not save file !");
        }
    }

    public void LoadJson()
    {
        if (CheckFile()) {
            PlayerData data = DataService.LoadData<PlayerData>("/player-stats.json", true);
            Level = data.Level;
            Xp = data.Xp;
            Money = data.Money;
            //foreach (var item in data.Inventory)
            //{
            //    Inventory.Add(item.ID);
            //}
            //foreach (var item in data.ItemLocked)
            //{
            //    ItemLocked.Add(item.ID);
            //}
            //foreach (var item in data.ActiveQuests)
            //{
            //    ActiveQuests.Add(item.ID);
            //}
            //foreach (var item in data.FinishedQuests)
            //{
            //    FinishedQuests.Add(item.ID);
            //}
            //foreach (var item in data.UnlockedQuest)
            //{
            //    QuestUnlocked.Add(item.ID);

            //}
            //foreach (var item in data.LockedQuest)
            //{
            //    QuestLocked.Add(item.ID); 
            //}
        }

        else
        {
            Debug.LogError("Could not read file");
        }

    }

    public bool CheckFile()
    {
        try
        {
            PlayerData data = DataService.LoadData<PlayerData>("/player-stats.json", true);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
