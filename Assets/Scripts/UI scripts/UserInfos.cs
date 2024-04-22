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
        _mXpBar.OnLevelUp -= RestartPosition;
        _mXpBar.OnLevelUp -= UpdateLevel;
    }

    private void LoadLevel()
    {
        _mPlayerLevel.text = GameManager.instance.Player.Level.ToString();
        _mPlayerStars.text = GameManager.instance.Player.Xp.ToString();
    }

    private void UpdateMoneyText()
    {
        MoneyManager.Instance.CurrentMoney = GameManager.instance.Player.Money;
        _mMoneyText.text = MoneyManager.Instance.CurrentMoney.ToString();
        Debug.Log(GameManager.instance.Player.Money.ToString());
    }

    private void UpdateLevel()
    {
        _mPlayerStars.text = GameManager.instance.Player.Xp.ToString();
        _mPlayerLevel.text = GameManager.instance.Player.Level.ToString();
        
        _mProgressBar.transform.position += _mProgressBar.transform.right * ((float) GameManager.instance.Player.Xp/ 5 * 3);
        //StartCoroutine(MoveProgressBar());
    }

    /*private IEnumerator MoveProgressBar(float amount)
    {
        while (_mStartPosition.x < _mStartPosition.x + 3 * amount) 
        {
            _mProgressBar.transform.position += _mProgressBar.transform.right * (amount * 3 * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }
    }*/

    private void RestartPosition()
    {
        _mProgressBar.transform.position = _mStartPosition;
    }
}
