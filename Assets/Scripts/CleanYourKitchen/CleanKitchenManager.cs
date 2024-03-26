using UnityEngine;

public class CleanKitchenManager : MiniGameManager
{
    [SerializeField] private CleanYourKitchenInteractableManager _interactableManager;
    private void Start()
    {
        Amount = 0;
        _interactableManager.OnRoachDeath += IncrementQuestAmount;
    }

    private void OnDestroy()
    {
        _interactableManager.OnRoachDeath -= IncrementQuestAmount;
    }
}
