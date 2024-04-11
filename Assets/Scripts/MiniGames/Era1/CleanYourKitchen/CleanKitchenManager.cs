using TMPro;
using UnityEngine;

public class CleanKitchenManager : MiniGameManager
{
    [SerializeField] private CleanYourKitchenInteractableManager _interactableManager;
    [SerializeField] private TextMeshProUGUI _roachRemainder;
    [SerializeField] private TextMeshProUGUI _roachAmount;
    [SerializeField] private GameObject _clickAnim;
    private int _numOfCockroaches;
    private Vector3 _clickAnimOffset;
    private Vector3 _clickAnimPos; 
    public override void Awake()
    {
        base.Awake();
        _numOfCockroaches = GameManager.instance.FasterLevel + 3;
        _interactableManager.NumOfCockroach = _numOfCockroaches;
    }

    private void Start()
    {
        Amount = 0;
        _clickAnimOffset = new Vector3(0f,0f,10f); 
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
        _clickAnimPos = pos += _clickAnimOffset;
    }

    public void PlayTapAnim2()
    {
        Debug.Log("nigga");
        if (_clickAnim.activeSelf) _clickAnim.SetActive(false);
        _clickAnim.transform.position = _clickAnimPos;
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
