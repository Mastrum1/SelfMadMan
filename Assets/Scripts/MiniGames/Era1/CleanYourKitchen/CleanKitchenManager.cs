using System.Threading;
using TMPro;
using UnityEngine;

public class CleanKitchenManager : MiniGameManager
{
    [SerializeField] private CleanYourKitchenInteractableManager _interactableManager;
    [SerializeField] private TextMeshProUGUI _roachRemainder;
    [SerializeField] private TextMeshProUGUI _roachAmount;
    [SerializeField] private GameObject _clickAnim;
    private int _numOfCockroaches;
    private AudioManager _audioManager;

    public override void Awake()
    {
        base.Awake();
        _numOfCockroaches = GameManager.instance.FasterLevel + 3;
        _interactableManager.NumOfCockroach = _numOfCockroaches;
    }

    private void Start()
    {
        _audioManager = AudioManager.Instance;
        _roachRemainder.text = Amount.ToString();
        _roachAmount.text = "/" + _numOfCockroaches;
        _interactableManager.OnRoachDeath += OnGameEnd;
    }

    public override void Update()
    {
        base.Update();

        _roachRemainder.text = Amount.ToString();
    }

    public void PlayTapAnim(Vector3 pos)
    {
        _audioManager.PlaySFX(1);
        if (_clickAnim.activeSelf) _clickAnim.SetActive(false); 
        _clickAnim.transform.position = new Vector3(pos.x, pos.y, 0);
        _clickAnim.SetActive(true);
    }

    private void OnGameEnd()
    {
        Amount++;
        if (Amount != _numOfCockroaches) return;
        
        _interactableManager.DisableRoaches();
        EndMiniGame(true, miniGameScore);
    }

    private void OnDestroy()
    {
        _interactableManager.OnRoachDeath -= OnGameEnd;
    }
}
