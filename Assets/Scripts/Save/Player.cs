using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

[System.Serializable]
public class Player : MonoBehaviour
{

    public event Action<PlayerData> OnDataLoad;

    private readonly PlayerData playerData = new PlayerData();

    private readonly IDataService DataService = new JsonData();

    private MiniGameSelector _mMiniGameSelectorInstance;

    private QuestManager _mQuestManagerInstance;

    [SerializeField] private MiniGameSelector _mMiniGameSelector;

    [SerializeField] private QuestManager _mQuestManager;

    public List<int> FirstQuests { get => _mFirstQuests; private set => _mFirstQuests = value; }

    [SerializeField] private List<int> _mFirstQuests;

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

    public List<int> Inventory { get => _mInventory; private set => _mInventory = value; }

    [SerializeField] private List<int> _mInventory;

    public List<Items> ItemLocked { get => _mItemLocked; private set => _mItemLocked = value; }

    [SerializeField] private List<Items> _mItemLocked;
    public List<int> ActiveQuests { get => _mActiveQuests; private set => _mActiveQuests = value; }

    [SerializeField] private List<int> _mActiveQuests;

    public List<int> CompletedQuests { get => _mCompletedQuests; private set => _mCompletedQuests = value; }

    [SerializeField] private List<int> _mCompletedQuests;

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

    public void Awake()
    {
        _mMiniGameSelectorInstance = MiniGameSelector.instance;

        _mQuestManagerInstance = QuestManager.instance;

        _mQuestManagerInstance.OnAddActiveQuest += AddActiveQuests;
        _mQuestManagerInstance.OnRemoveActiveQuest += RemoveActiveQuests;
        _mQuestManagerInstance.OnQuestComplete += QuestComplete;
        _mQuestManagerInstance.OnUnlockQuest += UnlockQuest;
        _mQuestManagerInstance.OnLockQuest += RemoveUnlockQuest;
        _mQuestManagerInstance.OnQuestFinished += RemoveCompleteQuests;


    }

    public void OnDisable()
    {
        _mQuestManagerInstance.OnAddActiveQuest -= AddActiveQuests;
        _mQuestManagerInstance.OnRemoveActiveQuest -= RemoveActiveQuests;
        _mQuestManagerInstance.OnQuestComplete -= QuestComplete;
        _mQuestManagerInstance.OnUnlockQuest -= UnlockQuest;
        _mQuestManagerInstance.OnLockQuest -= RemoveUnlockQuest;
        _mQuestManagerInstance.OnQuestFinished -= RemoveCompleteQuests;


    }

    public void LoadJson()
    {
        if (CheckFile())
        {
            PlayerData data = DataService.LoadData<PlayerData>("/player-stats.json", false);
            Level = data.Level;
            Xp = data.Xp;
            Money = data.Money;

            VolumeMusic = data.VolumeMusic;
            VolumeFX = data.VolumeFX;

            Language = data.Language;

            AllEra1 = data.AllEra1;
            AllEra2 = data.AllEra2;
            AllEra3 = data.AllEra3;

            foreach (var item in data.Inventory)
            {
                Inventory.Add(item);
                ItemLocked.Remove(ItemLocked[item]);
            }

            foreach (var item in data.ActiveQuests)
            {
                if (!ActiveQuests.Contains(item))
                    ActiveQuests.Add(item);

                AllQuest[item].QuestDispo = Quests.QuestBaseDispo.Unlocked;
                AllQuest[item].QuestCompletionState = QuestManager.CompletionState.Selected;
            }
            foreach (var item in data.CompletedQuests)
            {
                if (!CompletedQuests.Contains(item))
                    CompletedQuests.Add(item);

                AllQuest[item].QuestDispo = Quests.QuestBaseDispo.Unlocked;
                AllQuest[item].QuestCompletionState = QuestManager.CompletionState.Complete;

            }
            foreach (var item in data.QuestUnlocked)
            {
                if (!QuestUnlocked.Contains(item))
                    QuestUnlocked.Add(item);

                AllQuest[item].QuestDispo = Quests.QuestBaseDispo.Unlocked;
                AllQuest[item].QuestCompletionState = QuestManager.CompletionState.NotSelected;

            }

            _mMiniGameSelector.LoadEra(AllEra1, AllEra2, AllEra3);
            _mQuestManager.LoadQuests(AllQuest, ActiveQuests);
        }

        else
        {
            SaveJson();
        }

    }

    public void AddActiveQuests(int ID)
    {

        ActiveQuests.Add(ID);
        RemoveUnlockQuest(ID);
    }

    public void RemoveActiveQuests(int ID)
    {
        ActiveQuests.Remove(ID);
        UnlockQuest(ID);
    }

    public void QuestComplete(int ID)
    {
        CompletedQuests.Add(ID);
        RemoveActiveQuests(ID);
    }

    public void RemoveCompleteQuests(int ID)
    {
        CompletedQuests.Remove(ID);
        UnlockQuest(ID);
    }

    public void UnlockQuest(int ID)
    {
        QuestUnlocked.Add(ID);
    }

    public void RemoveUnlockQuest(int ID)
    {
        QuestUnlocked.Remove(ID);
    }

    //public void AddItemInInventory()
    //{

    //}

    //public void UnlockQuests()
    //{

    //}
    public bool CheckFile()
    {
        if (!File.Exists(Application.persistentDataPath + "/player-stats.json"))
        {
            return false;
        }
        return true;
    }
}
