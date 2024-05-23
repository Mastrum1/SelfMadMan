using System.Collections.Generic;
using static GameManager;
using static Player;

[System.Serializable]
public class PlayerData
{
    public bool clearAllSave;

    public int Level;

    public int Xp;
    public int Hearts;

    public int Money;

    public bool VolumeMusic;

    public bool VolumeFX;

    public string Language;

    public int BestScore;

    public bool alreadyPlayedTutorial = false;

    public List<int> UnlockedCinematics = new List<int>();

    public List<Player.QuestSave> ActiveQuests = new List<Player.QuestSave>();

    public List<TrophySave> AllTrophy = new List<TrophySave>();

    public List<Player.QuestSave> CompletedQuests = new List<Player.QuestSave>();

    public List<int> ActivesFurnitures = new List<int>();

    public List<Player.AllFurnituresSaveClass> allFurnituresSave = new List<Player.AllFurnituresSaveClass>();

    public List<int> QuestUnlocked = new List<int>();

    public List<GameManager.EraData> ErasData = new List<GameManager.EraData>();

    public List<int> AllEra1 = new List<int>();

    public List<int> AllEra2 = new List<int>();

    public List<int> AllEra3 = new List<int>();

    public bool IntroPlayed = false;
    public void SaveData(Player player)
    {
        Level = player.Level;
        Xp = player.Xp;
        Money = player.Money;
        Hearts = player.Hearts;
        BestScore = player.BestScore;

        VolumeMusic = player.VolumeMusic;
        VolumeFX = player.VolumeFX;

        Language = player.Language;

        ErasData = player.ErasData;

        IntroPlayed = player.IntroPlayed;

        clearAllSave = player.clearAllSave;

        alreadyPlayedTutorial = player.alreadyPlayedTutorial;

        SaveCinematics(player.UnlockedCinematics);

        for (int i = 0; i < player.AllEra1.Count; i++)
        {
            if (player.AllEra1[i].Locked == false && !AllEra1.Contains(i))
                AllEra1.Add(i);
        }

        for (int i = 0; i < player.AllEra2.Count; i++)
        {
            if (player.AllEra2[i].Locked == false && !AllEra2.Contains(i))
                AllEra2.Add(i);
        }

        for (int i = 0; i < player.AllEra3.Count; i++)
        {
            if (player.AllEra3[i].Locked == false && !AllEra3.Contains(i))
                AllEra3.Add(i);
        }

        allFurnituresSave = player.AllFurnituresSave;
        ActivesFurnitures = player.ActivesFurnitures;

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
            AllTrophy.Add(new TrophySave(item.TrophySO.ID, item.TrophyCompletionState, item.CurrentAmount));
        }
    }



    public void InitEras()
    {
        ErasData.Add(new EraData(true, 0));
        ErasData.Add(new EraData(false, 250));
        ErasData.Add(new EraData(false, 500));
    }

    public void FirstSaveData(Player player)
    {
        Level = 1;
        Xp = 0;
        Money = 0;
        BestScore = 0;

        VolumeMusic = true;
        VolumeFX = true;

        Language = "fr";

        clearAllSave = false;

        for (int i = 0; i < player.AllEra1.Count; i++)
        {
            if (player.AllEra1[i].Locked == false)
                AllEra1.Add(i);
        }

        for (int i = 0; i < player.AllEra2.Count; i++)
        {
            if (player.AllEra2[i].Locked == false)
                AllEra2.Add(i);
        }

        for (int i = 0; i < player.AllEra3.Count; i++)
        {
            if (player.AllEra3[i].Locked == false)
                AllEra3.Add(i);
        }

        InitEras();

        allFurnituresSave = player.AllFurnituresSave;

        QuestUnlocked = player.FirstQuests;
        SaveAllTrophyQuest(player.AllTrophy);

    }


}
