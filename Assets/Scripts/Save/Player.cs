using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

[System.Serializable]
public class Player : MonoBehaviour
{
    [System.Serializable]
    public class Cinematics
    {
        public int ID;
        public VideoClip clip;
    }

    public event Action<PlayerData> OnDataLoad;

    private readonly PlayerData playerData = new PlayerData();

    private readonly IDataService DataService = new JsonData();

    private QuestManager _mQuestManagerInstance;

    public int BestScore { get => _mBestScore; private set => _mBestScore = value; }

    [SerializeField] private int _mBestScore;

    public int Level { get => _mLevel; private set => _mLevel = value; }

    [SerializeField] private int _mLevel;

    public int Xp { get => _mXp; private set => _mXp = value; }

    [SerializeField] private int _mXp;

    public int Money { get => _mMoney; private set => _mMoney = value; }

    [SerializeField] private int _mMoney;

    public int VolumeMusic { get => _mVolumeMusic; private set => _mVolumeMusic = value; }

    [SerializeField] private int _mVolumeMusic;

    public int VolumeFX { get => _mVolumeFX; private set => _mVolumeFX = value; }

    [SerializeField] private int _mVolumeFX;

    public string Language { get => _mLanguage; private set => _mLanguage = value; }

    [SerializeField] private string _mLanguage;

    public List<Cinematics> AllCinematics { get => _mAllCinematics; private set => _mAllCinematics = value; }

    [SerializeField] private List<Cinematics> _mAllCinematics;

    public List<Cinematics> UnlockedCinematics { get => _mUnlockedCinematics; private set => _mUnlockedCinematics = value; }

    [SerializeField] private List<Cinematics> _mUnlockedCinematics;


    public List<Items> Inventory { get => _mInventory; private set => _mInventory = value; }

    [SerializeField] private List<Items> _mInventory;

    public List<Items> ItemLocked { get => _mItemLocked; private set => _mItemLocked = value; }

    [SerializeField] private List<Items> _mItemLocked;

    public List<int> FirstQuests { get => _mFirstQuests; private set => _mFirstQuests = value; }

    [SerializeField] private List<int> _mFirstQuests;
    public List<QuestManager.Quest> ActiveQuests { get => _mActiveQuests; private set => _mActiveQuests = value; }

    [SerializeField] private List<QuestManager.Quest> _mActiveQuests;

    public List<QuestManager.Quest> CompletedQuests { get => _mCompletedQuests; private set => _mCompletedQuests = value; }

    [SerializeField] private List<QuestManager.Quest> _mCompletedQuests;

    public List<int> QuestUnlocked { get => _mQuestUnlocked; private set => _mQuestUnlocked = value; }

    [SerializeField] private List<int> _mQuestUnlocked;

    public List<QuestManager.Quest> AllQuest { get => _mAllQuest; private set => _mAllQuest = value; }

    [SerializeField] private List<QuestManager.Quest> _mAllQuest;

    public List<MinigameScene> AllEra1 { get => _mAllEra1; private set => _mAllEra1 = value; }

    [SerializeField] private List<MinigameScene> _mAllEra1;

    public List<MinigameScene> AllEra2 { get => _mAllEra2; private set => _mAllEra2 = value; }

    [SerializeField] private List<MinigameScene> _mAllEra2;

    public List<MinigameScene> AllEra3 { get => _mAllEra3; private set => _mAllEra3 = value; }

    [SerializeField] private List<MinigameScene> _mAllEra3;



    public void SaveJson()
    {
        bool firstSave = false;
        if (!CheckFile())
        {
            firstSave = true;
            playerData.FirstSaveData(this);

        }
        else
        {
            playerData.SaveData(this);
        }
        if (DataService.SaveData("/player-stats.json", playerData, false))
        {
            if (firstSave)
            {
                LoadJson();
            }
        }
        else
        {
            Debug.LogError("Could not save file !");
        }
    }

    public void OnDisable()
    {
        QuestManager.instance.OnAddActiveQuest -= AddActiveQuests;
        QuestManager.instance.OnRemoveActiveQuest -= RemoveActiveQuests;
        QuestManager.instance.OnQuestComplete -= QuestComplete;
        QuestManager.instance.OnUnlockQuest -= UnlockQuest;
        QuestManager.instance.OnLockQuest -= RemoveUnlockQuest;
        QuestManager.instance.OnQuestFinished -= RemoveCompleteQuests;


    }

    public void LoadJson()
    {
        QuestManager.instance.OnAddActiveQuest += AddActiveQuests;
        QuestManager.instance.OnRemoveActiveQuest += RemoveActiveQuests;
        QuestManager.instance.OnQuestComplete += QuestComplete;
        QuestManager.instance.OnUnlockQuest += UnlockQuest;
        QuestManager.instance.OnLockQuest += RemoveUnlockQuest;
        QuestManager.instance.OnQuestFinished += RemoveCompleteQuests;

        if (CheckFile())
        {
            PlayerData data = DataService.LoadData<PlayerData>("/player-stats.json", false);
            BestScore = data.BestScore;
            Level = data.Level;
            Xp = data.Xp;
            Money = data.Money;

            VolumeMusic = data.VolumeMusic;
            VolumeFX = data.VolumeFX;

            Language = data.Language;

            foreach (var item in data.UnlockedCinematics)
            {
                UnlockedCinematics.Add(AllCinematics[item]);
                AllCinematics.Remove(AllCinematics[item]);
            }

            AllEra1 = data.AllEra1;
            AllEra2 = data.AllEra2;
            AllEra3 = data.AllEra3;

            foreach (var item in data.Inventory)
            {
                Inventory.Add(item);
                ItemLocked.Remove(item);
            }

            foreach (var item in data.ActiveQuests)
            {
                if (!ActiveQuests.Contains(item))
                    ActiveQuests.Add(item);

                AllQuest[item.QuestSO.ID] = item;
            }
            foreach (var item in data.CompletedQuests)
            {
                if (!CompletedQuests.Contains(item))
                    CompletedQuests.Add(item);

                AllQuest[item.QuestSO.ID] = item;

            }
            foreach (var item in data.QuestUnlocked)
            {
                if (!QuestUnlocked.Contains(item))
                    QuestUnlocked.Add(item);

                AllQuest[item].QuestDispo = Quests.QuestBaseDispo.Unlocked;
                AllQuest[item].QuestCompletionState = QuestManager.CompletionState.NotSelected;

            }

            //MiniGameSelector.instance.LoadEra(AllEra1, AllEra2, AllEra3);
            QuestManager.instance.LoadQuests(AllQuest, ActiveQuests);
        }

        else
        {
            SaveJson();
        }

    }

    public void UpdateBestScore(int newBestScore)
    {
        BestScore = newBestScore;
    }

    public void AddActiveQuests(QuestManager.Quest quest)
    {
        Debug.Log("test");

        ActiveQuests.Add(quest);
        RemoveUnlockQuest(quest.QuestSO.ID);
    }

    public void RemoveActiveQuests(QuestManager.Quest quest)
    {
        ActiveQuests.Remove(quest);
        UnlockQuest(quest.QuestSO.ID);
    }

    public void QuestComplete(QuestManager.Quest quest)
    {
        CompletedQuests.Add(quest);
        ActiveQuests.Remove(quest);
    }

    public void RemoveCompleteQuests(QuestManager.Quest quest)
    {
        CompletedQuests.Remove(quest);
        UnlockQuest(quest.QuestSO.ID);
    }

    public void UnlockQuest(int ID)
    {
        QuestUnlocked.Add(ID);
    }

    public void RemoveUnlockQuest(int ID)
    {
        QuestUnlocked.Remove(ID);
    }

    public void NewCurrency(int NewCurrency)
    {
        Money = NewCurrency;
    }

    public void AddStars(int Reward)
    {
        Xp += Reward;
    }

    public void ResetStars()
    {
        Xp = 0;
    }

    public void LvlUp()
    {
        Level++;
    }

    public void ChangeMusicVolume(float Value)
    {
        VolumeMusic = (int)Value; ;
    }

    public void ChangeSFXVolume(float Value)
    {

        VolumeFX = (int)Value;
    }

    public void AddItemInInventory(Items Item)
    {
        Inventory.Add(Item);
        ItemLocked.Remove(Item);
    }

    public void RemoveItemToInventory(Items Item)
    {
        Inventory.Remove(Item);
        ItemLocked.Add(Item);
    }

    public void AddUsableItemInInventory(Items item)
    {
        Inventory.Add(item);
    }

    public void RemoveUsableItemInInventory(Items item)
    {
        Inventory.Remove(item);
    }

    public void AddUnlockedCinematics(Cinematics cinematics)
    {
        UnlockedCinematics.Add(cinematics);
        AllCinematics.Remove(cinematics);
    }

    public void RemoveUnlockedCinematics(Cinematics cinematics)
    {
        UnlockedCinematics.Remove(cinematics);
        AllCinematics.Add(cinematics);
    }

    public void UnlockMinigame(int era, int minigame)
    {
        switch (era)
        {
            case 1:
                AllEra1[minigame].Unlock();
                break;
            case 2:
                AllEra2[minigame].Unlock();
                break;
            case 3:
                AllEra3[minigame].Unlock();
                break;

        }
    }

    public bool CheckFile()
    {
        if (!File.Exists(Application.persistentDataPath + "/player-stats.json"))
        {
            return false;
        }
        return true;
    }
}
