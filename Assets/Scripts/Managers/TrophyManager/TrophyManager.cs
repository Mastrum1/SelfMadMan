using System;
using System.Collections.Generic;
using UnityEngine;

public class TrophyManager : MonoBehaviour
{
    public static TrophyManager Instance;
    
    public event Action<Trophy> OnTrophyComplete;

    public event Action<int> OnReward;
    public enum CompletionState
    {
        NotComplete, Complete
    }
    
    [Serializable]
    public class Trophy
    {
        [SerializeField] private Trophies _trophyS0;
        public Trophies TrophySO { get => _trophyS0; set => _trophyS0 = value; }

        [SerializeField] private CompletionState _trophyCompletionState;
        public CompletionState TrophyCompletionState { get => _trophyCompletionState; set => _trophyCompletionState = value; }
        
        private int _goal;
        public int Goal { get => _goal; set => _goal = value; }

        private int _currentAmount;
        public int CurrentAmount { get => _currentAmount; set => _currentAmount = value; }

        public Trophy(Trophies trophies, CompletionState completionState, int goal, int currentAmount)
        {
            TrophySO = trophies;
            TrophyCompletionState = completionState;
            Goal = goal;
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
        _trophyList = trophies;
    }

    public void CheckTrophyCompletion(Trophy trophy)
    {
        if (trophy.CurrentAmount < trophy.Goal) return;
        
        trophy.TrophyCompletionState = CompletionState.Complete;
        OnTrophyComplete?.Invoke(trophy);
    }

    public void ClaimReward(Trophy trophy)
    {
        OnReward?.Invoke(trophy.TrophySO.reward);
        //Add money to wallet;
    }
}
