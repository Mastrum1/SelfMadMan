using System.Collections.Generic;
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

        foreach (var item in player.UnlockedCinematics)
        {
            UnlockedCinematics.Add(item.ID);
        }

        AllEra1 = player.AllEra1;
        AllEra2 = player.AllEra2;
        AllEra3 = player.AllEra3;

        Inventory = player.Inventory;

        foreach (var item in player.ItemLocked)
        {
            ItemLocked.Add(item);
        }

        ActiveQuests = new List<Player.QuestSave>();

        foreach (var item in player.ActiveQuests)
        {
            ActiveQuests.Add(new Player.QuestSave(item.QuestSO.ID, item.QuestCompletionState, item.QuestDispo, item.Difficulty, item.MaxAmount, item.CurrentAmount));
        }

        CompletedQuests = new List<Player.QuestSave>();

        foreach (var item in player.CompletedQuests)
        {
            CompletedQuests.Add(new Player.QuestSave(item.QuestSO.ID, item.QuestCompletionState, item.QuestDispo, item.Difficulty, item.MaxAmount, item.CurrentAmount));
        }

        AllTrophy = new List<TrophySave>();
        foreach (var item in player.AllTrophy)
        {
            AllTrophy.Add(new TrophySave(item.TrophySO.ID, item.TrophyCompletionState, item.Goal, item.CurrentAmount));
        }

        QuestUnlocked = player.QuestUnlocked;
    }

    private void InitEras()
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

        QuestUnlocked = player.FirstQuests;

    }


}
