using System.Collections.Generic;
using UnityEditor.Localization.Platform.Android;
using UnityEngine;
using static GameManager;
using static Player;

[System.Serializable]
public class PlayerData
{

    public int Level;

    public int Xp;

    public int Money;

    public int VolumeMusic;

    public int VolumeFX;

    public string Language;

    public int BestScore;

    public InventoryClass Inventory;

    public List<int> UnlockedCinematics = new List<int>();

    public List<Player.QuestSave> ActiveQuests = new List<Player.QuestSave>();

    public List<TrophySave> AllTrophy = new List<TrophySave>();

    public List<Player.QuestSave> CompletedQuests = new List<Player.QuestSave>();

    public List<FournituresClass> ItemLocked = new List<FournituresClass>();

    public List<int> QuestUnlocked = new List<int>();

    public List<GameManager.EraData> EraData = new List<GameManager.EraData>();

    public List<MinigameScene> AllEra1 = new List<MinigameScene>();

    public List<MinigameScene> AllEra2 = new List<MinigameScene>();

    public List<MinigameScene> AllEra3 = new List<MinigameScene>();

    public void SaveData(Player player)
    {
        Level = player.Level;
        Xp = player.Xp;
        Money = player.Money;

        BestScore = player.BestScore;

        VolumeMusic = player.VolumeMusic;
        VolumeFX = player.VolumeFX;

        Language = player.Language;

        EraData = player.EraData;

        SaveCinematics(player.UnlockedCinematics);

        AllEra1 = player.AllEra1;
        AllEra2 = player.AllEra2;
        AllEra3 = player.AllEra3;

        Inventory = player.Inventory;

        ItemLocked = player.ItemLocked;

        ActiveQuests = new List<Player.QuestSave>();

        SaveActiveQuest(player.ActiveQuests);

        CompletedQuests = new List<Player.QuestSave>();

        SaveCompletedQuest(player.CompletedQuests);

        AllTrophy = new List<TrophySave>();

        SaveAllTrophyQuest(player.AllTrophy);

        QuestUnlocked = player.QuestUnlocked;
    }

    public void SaveCinematics(List<Cinematics> CinematicsToSave)
    {
        foreach (var item in CinematicsToSave)
        {
            UnlockedCinematics.Add(item.ID);
        }
    }

    public void SaveActiveQuest(List<QuestManager.Quest> ActiveQuestsToSave)
    {
        foreach (var item in ActiveQuestsToSave)
        {
            ActiveQuests.Add(new Player.QuestSave(item.QuestSO.ID, item.QuestCompletionState, item.QuestDispo, item.Difficulty, item.MaxAmount, item.CurrentAmount));
        }
    }

    public void SaveCompletedQuest(List<QuestManager.Quest> CompletedQuestsToSave)
    {
        foreach (var item in CompletedQuestsToSave)
        {
            CompletedQuests.Add(new Player.QuestSave(item.QuestSO.ID, item.QuestCompletionState, item.QuestDispo, item.Difficulty, item.MaxAmount, item.CurrentAmount));
        }
    }
    public void SaveAllTrophyQuest(List<TrophyManager.Trophy> AllTrophyToSave)
    {
        foreach (var item in AllTrophyToSave)
        {
            AllTrophy.Add(new TrophySave(item.TrophySO.ID, item.TrophyCompletionState, item.Goal, item.CurrentAmount));
        }
    }



    public void InitEras()
    {
        EraData.Add(new EraData(true, 0));
        EraData.Add(new EraData(false, 1000));
        EraData.Add(new EraData(false, 2000));
    }

    public void FirstSaveData(Player player)
    {
        Level = 1;
        Xp = 0;
        Money = 0;
        BestScore = 0;

        VolumeMusic = 100;
        VolumeFX = 100;

        Language = "fr";

        AllEra1 = player.AllEra1;
        AllEra2 = player.AllEra2;
        AllEra3 = player.AllEra3;

        InitEras();

        Inventory = new InventoryClass();
        Inventory.Fournitures = new List<FournituresClass>();
        Inventory.UsableItems = new List<UsableItem>();

        QuestUnlocked = player.FirstQuests;

    }


}
