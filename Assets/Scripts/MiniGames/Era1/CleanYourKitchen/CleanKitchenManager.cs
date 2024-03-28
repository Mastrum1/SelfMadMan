using UnityEngine;

public class CleanKitchenManager : MiniGameManager
{
    [SerializeField] private CleanYourKitchenInteractableManager _interactableManager;
    [SerializeField] private int _numOfCockroaches;

    public override void Awake()
    {
        base.Awake();
        _interactableManager.NumOfCockroach = _numOfCockroaches;
    }

    private void Start()
    {
        Amount = 0;
        _interactableManager.OnRoachDeath += IncrementQuestAmount;
        _interactableManager.OnRoachDeath += OnGameEnd;
    }
    
    void OnGameEnd()
    {
        if (Amount == _numOfCockroaches)
        {
            EndMiniGame(true, miniGameScore);
        }
    }

    private void OnDestroy()
    {
        _interactableManager.OnRoachDeath -= IncrementQuestAmount;
        _interactableManager.OnRoachDeath += OnGameEnd;
    }
}
