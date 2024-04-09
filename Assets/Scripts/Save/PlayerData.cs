using System.Collections.Generic;

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

    public List<Items> Inventory = new List<Items>();

    public List<int> UnlockedCinematics = new List<int>();

    public List<Player.QuestSave> ActiveQuests = new List<Player.QuestSave>();

    public List<Player.QuestSave> CompletedQuests = new List<Player.QuestSave>();

    public List<int> QuestUnlocked = new List<int>();

    public List<MinigameScene> AllEra1 = new List<MinigameScene>();

    public List<MinigameScene> AllEra2 = new List<MinigameScene>();

    public List<MinigameScene> AllEra3 = new List<MinigameScene>();


    //public List<> cinematique;

    public void SaveData(Player player)
    {
        Level = player.Level;
        Xp = player.Xp;
        Money = player.Money;

        VolumeMusic = player.VolumeMusic;
        VolumeFX = player.VolumeFX;

        Language = player.Language;


        foreach (var item in player.UnlockedCinematics)
        {
            UnlockedCinematics.Add(item.ID);
        }

        AllEra1 = player.AllEra1;
        AllEra2 = player.AllEra2;
        AllEra3 = player.AllEra3;

        Inventory = player.Inventory;

        ActiveQuests = new List<Player.QuestSave>();

        foreach (var item in player.ActiveQuests)
        {
            ActiveQuests.Add(new Player.QuestSave(item.QuestSO, item.QuestCompletionState, item.QuestDispo, item.Sprite, item.Difficulty, item.MaxAmount, item.CurrentAmount));
        }

        CompletedQuests = new List<Player.QuestSave>();

        foreach (var item in player.CompletedQuests)
        {
            CompletedQuests.Add(new Player.QuestSave(item.QuestSO, item.QuestCompletionState, item.QuestDispo, item.Sprite, item.Difficulty, item.MaxAmount, item.CurrentAmount));
        }

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
