using TMPro;
using UnityEngine;

public class CleanKitchenManager : MiniGameManager
{
    [SerializeField] private CleanYourKitchenInteractableManager _interactableManager;
    [SerializeField] private TextMeshProUGUI _roachRemainder;
    [SerializeField] private TextMeshProUGUI _roachAmount;
    [SerializeField] private GameObject _clickAnim;
    private int _numOfCockroaches;

    public override void Awake()
    {
        base.Awake();
        _numOfCockroaches = GameManager.instance.FasterLevel + 3;
        _interactableManager.NumOfCockroach = _numOfCockroaches;
    }

    private void Start()
    {
        Amount = 0;
        _roachRemainder.text = Amount.ToString();
        _roachAmount.text = "/" + _numOfCockroaches;
        _interactableManager.OnRoachDeath += IncrementQuestAmount;
        _interactableManager.OnRoachDeath += OnGameEnd;
    }

    public override void Update()
    {
        base.Update();

        _roachRemainder.text = Amount.ToString();
    }

    public void PlayTapAnim(Vector3 pos)
    {
        if (_clickAnim.activeSelf) _clickAnim.SetActive(false); 
        _clickAnim.transform.position = new Vector3(pos.x, pos.y, 0);
        _clickAnim.SetActive(true);
    }

    private void OnGameEnd()
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
