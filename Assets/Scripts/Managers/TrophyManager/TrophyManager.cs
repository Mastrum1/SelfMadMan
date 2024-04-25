using System;
using System.Collections.Generic;
using UnityEngine;

public class TrophyManager : MonoBehaviour
{
    public static TrophyManager Instance;
    
    public event Action<Trophy> OnTrophyComplete;
    public event Action<Trophy, int> OnTrophyClaimed;
    public enum CompletionState
    {
        NotComplete, Complete, Claimed
    }
    
    [Serializable]
    public class Trophy
    {
        [SerializeField] private Trophies _trophyS0;
        public Trophies TrophySO { get => _trophyS0; set => _trophyS0 = value; }

        [SerializeField] private CompletionState _trophyCompletionState;
        public CompletionState TrophyCompletionState { get => _trophyCompletionState; set => _trophyCompletionState = value; }
        
        private int _currentAmount;
        public int CurrentAmount { get => _currentAmount; set => _currentAmount = value; }

        public Trophy(Trophies trophies, CompletionState completionState, int currentAmount)
        {
            TrophySO = trophies;
            TrophyCompletionState = completionState;
            CurrentAmount = currentAmount;
        }
    }

    [SerializeField] private List<Trophy> _trophyList;
    public List<Trophy> TrophyList => _trophyList;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void LoadTrophies(List<Trophy> trophies)
    {
        for (var i = 0; i < trophies.Count; i++)
        {
            _trophyList.Add(trophies[i]);
            CheckTrophyCompletion(_trophyList[i]);
        }
    }

    private void CheckTrophyCompletion(Trophy trophy)
    {
        if (trophy.TrophyCompletionState != CompletionState.NotComplete) return;
        
        if (trophy.CurrentAmount < trophy.TrophySO.goal) return;
        
        trophy.TrophyCompletionState = CompletionState.Complete;
        OnTrophyComplete?.Invoke(trophy);
    }

    public void AddTrophyAmount(int id, int amount)
    {
        if (_trophyList[id].TrophyCompletionState != CompletionState.NotComplete) return;
        
        _trophyList[id].CurrentAmount += amount;

        if (id == 3 && amount != _trophyList[id].TrophySO.goal)
        {
            _trophyList[id].CurrentAmount = 0;
        }
    }

    public void ClaimReward(Trophy trophy)
    {
        trophy.TrophyCompletionState = CompletionState.Claimed;
        OnTrophyClaimed?.Invoke(trophy, trophy.TrophySO.reward);
        MoneyManager.Instance.AddMoney(trophy.TrophySO.reward);
        MoneyManager.Instance.UpdateMoney();
    }
}
