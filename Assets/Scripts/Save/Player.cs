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
    [System.Serializable]
    public class TrophySave
    {
        [SerializeField] private int _TrophySOIndex;
        public int TrophySOIndex { get => _TrophySOIndex; set => _TrophySOIndex = value; }

        [SerializeField] private TrophyManager.CompletionState _trophyCompletionState;
        public TrophyManager.CompletionState TrophyCompletionState { get => _trophyCompletionState; set => _trophyCompletionState = value; }

        private int _goal;
        public int Goal { get => _goal; set => _goal = value; }

        private int _currentAmount;
        public int CurrentAmount { get => _currentAmount; set => _currentAmount = value; }

        public TrophySave(int TrophySOIndex, TrophyManager.CompletionState completionState, int goal, int currentAmount)
        {
            _TrophySOIndex = TrophySOIndex;
            TrophyCompletionState = completionState;
            Goal = goal;
            CurrentAmount = currentAmount;
        }
    }


    [System.Serializable]
    public class QuestSave
    {
        [SerializeField] private int _questSOIndex;
        public int QuestSOIndex { get => _questSOIndex; set => _questSOIndex = value; }

        [SerializeField] private QuestManager.CompletionState _questCompletionState;
        public QuestManager.CompletionState QuestCompletionState { get => _questCompletionState; set => _questCompletionState = value; }

        [SerializeField] private Quests.QuestBaseDispo _questDispo;
        public Quests.QuestBaseDispo QuestDispo { get => _questDispo; set => _questDispo = value; }

        private Quests.Difficulty _difficulty;
        public Quests.Difficulty Difficulty { get => _difficulty; set => _difficulty = value; }

        private int _maxAmount;
        public int MaxAmount { get => _maxAmount; set => _maxAmount = value; }

        private int _currentAmount;
        public int CurrentAmount { get => _currentAmount; set => _currentAmount = value; }

        public QuestSave(int QuestSOIndex, QuestManager.CompletionState questCompletionState, Quests.QuestBaseDispo questDispo, Quests.Difficulty difficulty, int maxAmount, int currentAmount)
        {
            _questSOIndex = QuestSOIndex;
            _questCompletionState = questCompletionState;
            _questDispo = questDispo;
            _difficulty = difficulty;
            _maxAmount = maxAmount;
            _currentAmount = currentAmount;
        }
    }

    [System.Serializable]
    public class InventoryClass
    {
        private List<UsableItem> _usableItems;
        public List<UsableItem> UsableItems { get => _usableItems; set => _usableItems = value; }

        private List<FournituresClass> _fournitures;
        public List<FournituresClass> Fournitures { get => _fournitures; set => _fournitures = value; }
    }

    [SerializeField] private bool _mLoadSaveMinigame = false;


    public event Action<PlayerData> OnDataLoad;

    private readonly PlayerData playerData = new PlayerData();

    private readonly IDataService DataService = new JsonData();

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


    public InventoryClass Inventory { get => _mInventory; private set => _mInventory = value; }

    [SerializeField] private InventoryClass _mInventory;

    public List<FournituresClass> ItemLocked { get => _mItemLocked; private set => _mItemLocked = value; }

    [SerializeField] private List<FournituresClass> _mItemLocked;

    public List<FournituresClassSO> AllFournituresSO { get => _mAllFournituresSO; private set => _mAllFournituresSO = value; }

    [SerializeField] private List<FournituresClassSO> _mAllFournituresSO;

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

    public List<TrophyManager.Trophy> AllTrophy { get => _mAllTrophy; private set => _mAllTrophy = value; }

    [SerializeField] private List<TrophyManager.Trophy> _mAllTrophy;

    public List<GameManager.EraData> EraData { get => _EraData; private set => _EraData = value; }

    private List<GameManager.EraData> _EraData = new List<GameManager.EraData>();

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
    }

    public void UpdateSaveFile(PlayerData data)
    {
        if (data.AllEra1.Count == 8 && data.AllEra1 != AllEra1)
        {
            AllEra1 = data.AllEra1;
        }
        else
        {
            data.AllEra1 = AllEra1;
        }

        if (data.AllEra2.Count == 8 && data.AllEra2 != AllEra2)
        {
            AllEra2 = data.AllEra2;
        }
        else
        {
            data.AllEra2 = AllEra2;
        }

        if (data.AllEra3.Count == 8 && data.AllEra3 != AllEra3)
        {
            AllEra3 = data.AllEra3;
        }
        else
        {
            data.AllEra3 = AllEra3;
        }

        if (data.UnlockedCinematics.Count != 0)
        {
            foreach (var item in data.UnlockedCinematics)
            {
                UnlockedCinematics.Add(AllCinematics[item]);
                AllCinematics.Remove(AllCinematics[item]);
            }
        }
        else
        {
            data.SaveCinematics(UnlockedCinematics);
        }

        if (data.BestScore != BestScore)
            BestScore = data.BestScore;
        else
            data.BestScore = BestScore;

        if (data.Level != Level)
            Level = data.Level;
        else
            data.Level = Level;

        if (data.Xp != 0)
        {
            Xp = data.Xp;
        }
        else
        {
            data.Xp = Xp;
        }

        if (data.Money != Money)
        {
            Money = data.Money;
        }
        else
        {
            data.Money = Money;
        }

        if (data.VolumeMusic != VolumeMusic)
        {
            VolumeMusic = data.VolumeMusic;
        }
        else
        {
            data.VolumeMusic = VolumeMusic;
        }

        if (data.VolumeFX != VolumeFX)
        {
            VolumeFX = data.VolumeFX;
        }
        else
        {
            data.VolumeFX = VolumeFX;
        }

        if (data.Language != null)
        {
            Language = data.Language;
        }
        else
        {
            data.Language = Language;
        }

        if (data.Inventory != null && data.Inventory.Fournitures != null && data.Inventory.Fournitures.Count != 0 || data.Inventory != null && data.Inventory.UsableItems != null && data.Inventory.UsableItems.Count != 0)
        {
            Inventory = data.Inventory;
        }
        else
        {
            data.Inventory = Inventory;
        }

        if (data.ItemLocked.Count != 0 && data.ItemLocked != ItemLocked)
        {
            ItemLocked = data.ItemLocked;
        }
        else
        {
            data.ItemLocked = ItemLocked;
        }


        if (data.ActiveQuests.Count != 0)
        {
            foreach (var item in data.ActiveQuests)
            {
                AllQuest[item.QuestSOIndex] = new QuestManager.Quest(AllQuest[item.QuestSOIndex].QuestSO, item.QuestCompletionState, item.QuestDispo, item.Difficulty, item.MaxAmount, item.CurrentAmount);
                ActiveQuests.Add(AllQuest[item.QuestSOIndex]);
            }
        }
        else
        {
            data.SaveActiveQuest(ActiveQuests);
        }

        if (data.CompletedQuests.Count != 0)
        {
            foreach (var item in data.CompletedQuests)
            {
                AllQuest[item.QuestSOIndex] = new QuestManager.Quest(AllQuest[item.QuestSOIndex].QuestSO, item.QuestCompletionState, item.QuestDispo, item.Difficulty, item.MaxAmount, item.CurrentAmount);
                CompletedQuests.Add(AllQuest[item.QuestSOIndex]);

            }
        }
        else
        {
            data.SaveCompletedQuest(CompletedQuests);
        }

        if (data.QuestUnlocked.Count != 0)
        {
            foreach (var item in data.QuestUnlocked)
            {
                if (!QuestUnlocked.Contains(item))
                    QuestUnlocked.Add(item);

                AllQuest[item].QuestDispo = Quests.QuestBaseDispo.Unlocked;
                AllQuest[item].QuestCompletionState = QuestManager.CompletionState.NotSelected;
            }
        }
        else
        {
        }

        if (data.AllTrophy.Count != 0)
        {
            foreach (var item in data.AllTrophy)
            {
                AllTrophy[item.TrophySOIndex] = new TrophyManager.Trophy(AllTrophy[item.TrophySOIndex].TrophySO, item.TrophyCompletionState, item.Goal, item.CurrentAmount);
            }
        }
        else
        {
            data.SaveAllTrophyQuest(AllTrophy);
        }

        if (data.EraData.Count == 3)
        {
            if (data.EraData[2].Unlocked == true || data.EraData[3].Unlocked == true)
            {
                EraData = data.EraData;
            }
        }
        else
        {
            data.InitEras();
            EraData = data.EraData;
        }
        SaveJson();

        LoadJson();
    }

    public void OnDisable()
    {
        QuestManager.Instance.OnAddActiveQuest -= AddActiveQuests;
        QuestManager.Instance.OnRemoveActiveQuest -= RemoveActiveQuests;
        QuestManager.Instance.OnQuestComplete -= QuestComplete;
        QuestManager.Instance.OnUnlockQuest -= UnlockQuest;
        QuestManager.Instance.OnLockQuest -= RemoveUnlockQuest;
        QuestManager.Instance.OnQuestFinished -= RemoveCompleteQuests;
        TrophyManager.Instance.OnTrophyComplete -= TrophyCompleted;
        TrophyManager.Instance.OnTrophyClaimed -= ClaimTrophy;

    }

    public void LoadJson()
    {
        QuestManager.Instance.OnAddActiveQuest += AddActiveQuests;
        QuestManager.Instance.OnRemoveActiveQuest += RemoveActiveQuests;
        QuestManager.Instance.OnQuestComplete += QuestComplete;
        QuestManager.Instance.OnUnlockQuest += UnlockQuest;
        QuestManager.Instance.OnLockQuest += RemoveUnlockQuest;
        QuestManager.Instance.OnQuestFinished += RemoveCompleteQuests;
        TrophyManager.Instance.OnTrophyComplete += TrophyCompleted;
        TrophyManager.Instance.OnTrophyClaimed += ClaimTrophy;

        if (CheckFile())
        {
            PlayerData data = DataService.LoadData<PlayerData>("/player-stats.json", true);

            if (data == default)
                data = DataService.LoadData<PlayerData>("/player-stats.json", false);


            if (data.EraData.Count == 0)
            {

                UpdateSaveFile(data);
            }
            else
            {
                BestScore = data.BestScore;
                Level = data.Level;
                Xp = data.Xp;
                Money = data.Money;

                VolumeMusic = data.VolumeMusic;
                VolumeFX = data.VolumeFX;

                Language = data.Language;

                EraData = data.EraData;

                foreach (var item in data.UnlockedCinematics)
                {
                    UnlockedCinematics.Add(AllCinematics[item]);
                    AllCinematics.Remove(AllCinematics[item]);
                }

                AllEra1 = data.AllEra1;
                AllEra2 = data.AllEra2;
                AllEra3 = data.AllEra3;

                Inventory = new InventoryClass();

                Inventory.UsableItems = new List<UsableItem>();

                Inventory.Fournitures = new List<FournituresClass>();

                Inventory = data.Inventory;

                Inventory.UsableItems = data.Inventory.UsableItems;
                Inventory.Fournitures = data.Inventory.Fournitures;

                foreach (var item in data.ItemLocked)
                {
                    ItemLocked.Add(item);
                }

                ActiveQuests = new List<QuestManager.Quest>();
                foreach (var item in data.ActiveQuests)
                {
                    AllQuest[item.QuestSOIndex] = new QuestManager.Quest(AllQuest[item.QuestSOIndex].QuestSO, item.QuestCompletionState, item.QuestDispo, item.Difficulty, item.MaxAmount, item.CurrentAmount);
                    ActiveQuests.Add(AllQuest[item.QuestSOIndex]);
                }
                CompletedQuests = new List<QuestManager.Quest>();
                foreach (var item in data.CompletedQuests)
                {
                    AllQuest[item.QuestSOIndex] = new QuestManager.Quest(AllQuest[item.QuestSOIndex].QuestSO, item.QuestCompletionState, item.QuestDispo, item.Difficulty, item.MaxAmount, item.CurrentAmount);
                    CompletedQuests.Add(AllQuest[item.QuestSOIndex]);

                }
                foreach (var item in data.QuestUnlocked)
                {
                    if (!QuestUnlocked.Contains(item))
                        QuestUnlocked.Add(item);

                    AllQuest[item].QuestDispo = Quests.QuestBaseDispo.Unlocked;
                    AllQuest[item].QuestCompletionState = QuestManager.CompletionState.NotSelected;
                }

                foreach (var item in data.AllTrophy)
                {
                    AllTrophy[item.TrophySOIndex] = new TrophyManager.Trophy(AllTrophy[item.TrophySOIndex].TrophySO, item.TrophyCompletionState, item.Goal, item.CurrentAmount);
                }

                if (_mLoadSaveMinigame)
                {
                    MiniGameSelector.instance.LoadEra(AllEra1, AllEra2, AllEra3);
                }
                GameManager.instance.LoadEraData(EraData);

                TrophyManager.Instance.LoadTrophies(AllTrophy);
                QuestManager.Instance.LoadQuests(AllQuest, ActiveQuests);

            }
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
        if (ActiveQuests.Contains(quest)) return;

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

    public void AddFournitureInInventory(FournituresClassSO Item)
    {
        Inventory.Fournitures.Add(new FournituresClass(Item.GetItemSOID()));
        ItemLocked.Remove(new FournituresClass(Item.GetItemSOID()));
    }

    public void RemoveFournitureInInventory(FournituresClassSO Item)
    {
        Inventory.Fournitures.Remove(Inventory.Fournitures[Inventory.Fournitures.IndexOf(new FournituresClass(Item.GetItemSOID()))]);
        ItemLocked.Add(new FournituresClass(Item.GetItemSOID()));
    }

    public void AddUsableItemInInventory(UsableItem item)
    {
        if (Inventory.UsableItems.Contains(item))
        {
            Inventory.UsableItems[Inventory.UsableItems.IndexOf(item)].Quantity += 1;
            return;
        }
        Inventory.UsableItems.Add(item);
    }

    public void UseUsableItem(UsableItem item)
    {
        Inventory.UsableItems[Inventory.UsableItems.IndexOf(item)].Quantity -= 1;

        if (Inventory.UsableItems[Inventory.UsableItems.IndexOf(item)].Quantity == 0)
        {
            Inventory.UsableItems.Remove(Inventory.UsableItems[Inventory.UsableItems.IndexOf(item)]);
        }
    }

    public bool SearchForUsableItem(UsableItem item)
    {
        return Inventory.UsableItems.Contains(item);
    }

    public bool SearchForFournitureItem(FournituresClass item)
    {
        return Inventory.Fournitures.Contains(item);
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
            case 0:
                AllEra1[minigame].Unlock();
                break;
            case 1:
                AllEra2[minigame].Unlock();
                break;
            case 2:
                AllEra3[minigame].Unlock();
                break;

        }
    }

    public void UnlockEra(int era)
    {
        EraData[era].UnlockEra();
    }

    public void TrophyCompleted(TrophyManager.Trophy trophy)
    {
        AllTrophy[trophy.TrophySO.ID] = trophy;
    }

    public void ClaimTrophy(TrophyManager.Trophy trophy, int reward)
    {
        AllTrophy[trophy.TrophySO.ID] = trophy;
        _mMoney += reward;
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
