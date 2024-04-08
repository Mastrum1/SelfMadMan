using TMPro;
using UnityEngine;

public class CleanKitchenManager : MiniGameManager
{
    [SerializeField] private CleanYourKitchenInteractableManager _interactableManager;
    [SerializeField] private TextMeshProUGUI _roachRemainder;
    private int _numOfCockroaches;

    public override void Awake()
    {
        base.Awake();
        _numOfCockroaches = GameManager.instance.FasterLevel + 3;
        _interactableManager.NumOfCockroach = _numOfCockroaches;
    }

    private void Start()
    {
        _roachRemainder.text = _numOfCockroaches.ToString();
        Amount = 0;
        _interactableManager.OnRoachDeath += IncrementQuestAmount;
        _interactableManager.OnRoachDeath += OnGameEnd;
    }

    public override void Update()
    {
        base.Update();

        _roachRemainder.text = (_numOfCockroaches - Amount).ToString();
    }

    public void PlayTapAnim(GameObject clickAnim)
    {
        //clickAnim.transform.position =
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
