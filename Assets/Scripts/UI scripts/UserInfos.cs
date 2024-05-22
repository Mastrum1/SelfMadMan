using System.Collections;
using TMPro;
using UnityEngine;

public class UserInfos : MonoBehaviour
{
    [SerializeField] private TMP_Text _mMoneyText;
    [SerializeField] private TMP_Text _mPlayerLevel;
    [SerializeField] private TMP_Text _mPlayerStars;
    [SerializeField] private TMP_Text _mPlayerName;
    [SerializeField] private GameObject _mProgressBar;
    [SerializeField] private XpBar _mXpBar;
    [SerializeField] private Vector3 _mStartPosition;

    [Header("Money Animation")]
    [SerializeField] private float _animDuration = 1.0f; // Durée de l'animation en secondes


    private int _currentMoney = 0; // Le score actuel affiché
    public int CurrentMoney { get => _currentMoney; set => _currentMoney = value; }
    private Coroutine _moneyCoroutine;
    private void Start()
    {
        UpdateMoneyText();
        LoadLevel();
    }

    private void OnEnable()
    {
        MoneyManager.Instance.OnUpdateMoney += UpdateMoneyText;
        GameManager.instance.OnUpdateLevel += UpdateLevel;
        //_mXpBar.OnLevelUp += RestartPosition;
        //_mXpBar.OnLevelUp += UpdateLevel;
    }

    private void OnDisable()
    {
        MoneyManager.Instance.OnUpdateMoney -= UpdateMoneyText;
        GameManager.instance.OnUpdateLevel -= UpdateLevel;
        //_mXpBar.OnLevelUp -= RestartPosition;
        //_mXpBar.OnLevelUp -= UpdateLevel;
    }

    private void LoadLevel()
    {
        _mPlayerLevel.text = GameManager.instance.Player.Level.ToString();
        _mPlayerStars.text = GameManager.instance.Player.Xp.ToString();
        _mProgressBar.transform.position += _mProgressBar.transform.right * ((float) GameManager.instance.Player.Xp/ 5 * 2.2f);

        UpdateName(GameManager.instance.Player.Level - 1);
    }

    private void UpdateMoneyText()
    {
        _currentMoney = MoneyManager.Instance.CurrentMoney;
        MoneyManager.Instance.CurrentMoney = GameManager.instance.Player.Money;
        UpdateMoney(GameManager.instance.Player.Money);
        _mMoneyText.text = MoneyManager.Instance.CurrentMoney.ToString();
        Debug.Log(GameManager.instance.Player.Money.ToString());
    }

    public void UpdateMoney(float newScore)
    {
        if (_moneyCoroutine != null)
        {
            StopCoroutine(_moneyCoroutine);
        }
        _moneyCoroutine = StartCoroutine(AnimateMoney(_currentMoney, newScore));
    }

    private IEnumerator AnimateMoney(int startScore, float endScore)
    {
        float duration = _animDuration; // Durée de l'animation en secondes
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            int displayScore = (int)Mathf.Lerp(startScore, endScore, t);
            _mMoneyText.text = displayScore.ToString();
            yield return null;
        }

        _currentMoney = (int)endScore;
        _mMoneyText.text = _currentMoney.ToString();
//GameManager.instance.TempMinigameScore = (int)endScore;
    }

    private void UpdateLevel()
    {
        _mPlayerStars.text = GameManager.instance.Player.Xp.ToString();
        _mPlayerLevel.text = GameManager.instance.Player.Level.ToString();
        
        RestetPosition();
        _mProgressBar.transform.position += _mProgressBar.transform.right * ((float) GameManager.instance.Player.Xp/ 5 * 2.2f);
        UpdateName(GameManager.instance.Player.Level - 1);
        
        //StartCoroutine(MoveProgressBar());
    }

    private void UpdateName(int name)
    {
        _mPlayerName.text = GameManager.instance.PlayerTitle[name];
    }

    /*private IEnumerator MoveProgressBar(float amount)
    {
        while (_mStartPosition.x < _mStartPosition.x + 3 * amount) 
        {
            _mProgressBar.transform.position += _mProgressBar.transform.right * (amount * 3 * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }
    }*/

    private void RestetPosition()
    {
        _mProgressBar.transform.position = _mStartPosition;
    }
}
