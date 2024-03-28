using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static QuestManager;


[System.Serializable]
public class PlayerData
{

    public int Level { get => _mLevel; private set => _mLevel = value; }

    public int _mLevel;

    public int Xp { get => _mXp; private set => _mXp = value; }

    public int _mXp;

    public int Money { get => _mMoney; private set => _mMoney = value; }

    public int _mMoney;

    public int VolumeMusic { get => _mVolumeMusic; private set => _mVolumeMusic = value; }

    public int _mVolumeMusic;

    public int VolumeFX { get => _mVolumeFX; private set => _mVolumeFX = value; }

    public int _mVolumeFX;

    public string Language { get => _mLanguage; private set => _mLanguage = value; }

    private string _mLanguage;

    public List<int> Inventory { get => _mInventory; private set => _mInventory = value; }

    private List<int> _mInventory = new List<int>();

    public List<int> ActiveQuests { get => _mActiveQuests; private set => _mActiveQuests = value; }

    private List<int> _mActiveQuests = new List<int>();

    public List<int> CompletedQuests { get => _mCompletedQuests; private set => _mCompletedQuests = value; }

    private List<int> _mCompletedQuests = new List<int>();

    public List<int> QuestUnlocked { get => _mQuestUnlocked; private set => _mQuestUnlocked = value; }

    private List<int> _mQuestUnlocked = new List<int>();

    public List<MinigameScene> AllEra1 { get => _mAllEra1; private set => _mAllEra1 = value; }

    private List<MinigameScene> _mAllEra1 = new List<MinigameScene>();

    public List<MinigameScene> AllEra2 { get => _mAllEra2; private set => _mAllEra2 = value; }

    private List<MinigameScene> _mAllEra2 = new List<MinigameScene>();

    public List<MinigameScene> AllEra3 { get => _mAllEra3; private set => _mAllEra3 = value; }

    private List<MinigameScene> _mAllEra3 = new List<MinigameScene>();

    //public List<> cinematique;


    public void SaveData(Player player)
    {
        Level = player.Level;
        Xp = player.Xp;
        Money = player.Money;

        VolumeMusic = player.VolumeMusic;
        VolumeFX = player.VolumeFX;

        Language = player.Language;

        AllEra1 = player.AllEra1;
        AllEra2 = player.AllEra2;
        AllEra3 = player.AllEra3;

        Inventory = player.Inventory;

        ActiveQuests = player.ActiveQuests;

        CompletedQuests = player.CompletedQuests;

        QuestUnlocked = player.QuestUnlocked;
    }

    public void FirstSaveData(Player player)
    {
        Level = 1;
        Xp = 0;
        Money = 0;

        VolumeMusic = 100;
        VolumeFX = 100;

        Language = "fr";

        AllEra1 = player.AllEra1;
        AllEra2 = player.AllEra2;
        AllEra3 = player.AllEra3;

        QuestUnlocked = player.FirstQuests;

    }


}
