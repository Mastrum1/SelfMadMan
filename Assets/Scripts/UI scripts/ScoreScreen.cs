using CW.Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour
{
    [SerializeField] private Animator _mJamesAnimator;
    [SerializeField] private Animator _UIAnimator;
    [SerializeField] private Animator _HeartAnimator;
    [SerializeField] private Image _jamesSprite;
    [SerializeField] private GameObject _mHighscoreTag;
    [SerializeField] private TMP_Text _mscoreTexts;
    [SerializeField] private TMP_Text _mHearts;
    [SerializeField] private TMP_Text _mMnigameCountTexts;
    [SerializeField] private TMP_Text _mTimerTexts;
    [SerializeField] private TMP_Text _bestScore;
    [SerializeField] private TMP_Text _coinsEarned;
    [SerializeField] private PanelOnClick _mPanelOnClick;
    [SerializeField] private GameObject _mPopup;
    [SerializeField] private GameObject _InputManager;
    [SerializeField] private GameObject _mQuestManager;
    [SerializeField] private RectTransform _mCoin;
    [SerializeField] private GameObject _mScoreGainCoin;

    private int Timer = 5;

    private bool _mPopupClosed = true;
    private bool _mContinue = false;
    private void Awake()
    {
        _InputManager.SetActive(false);
        GameManager.instance.WinScreenHandle += OnWinScreenDisplay;
        _mPanelOnClick.OnClick += OnPanelClicked;
    }


    void OnWinScreenDisplay(bool won, int era, int hearts, bool gameOver)
    {
        Debug.Log(hearts);
        _mscoreTexts.text = GameManager.instance.Score.ToString();
        _mMnigameCountTexts.text = "GAMES COMPLETED : " + GameManager.instance.MinigamesWon.ToString();
        _HeartAnimator.SetInteger("Hearts", hearts);
        _mJamesAnimator.SetBool("Idle", false);
        _mJamesAnimator.SetBool("Won", won);
        _mJamesAnimator.SetInteger("Era", era + 1);


        if (gameOver)
        {
            _InputManager.SetActive(true);
            StartCoroutine(OnGameOver());
        }
        else
        {
            StartCoroutine(ResetCharacter());
        }
    }

    void OnPanelClicked()
    {
        _mPopupClosed = true;
        _UIAnimator.SetBool("DisplayPopUp", false);
    }

    public void OnContinue()
    {
        if (GameManager.instance.Player.Hearts > 0 && Timer > 0)
        {
            _mContinue = true;
            Timer = 5;
            GameManager.instance.Player.Hearts--;
            _mPopupClosed = true;
            _UIAnimator.SetBool("DisplayPopUp", false);
            StartCoroutine(OnContinueAnim());
            _HeartAnimator.SetInteger("Hearts", 1);
            _HeartAnimator.SetBool("Revived", true);
            _InputManager.SetActive(false);
            StartCoroutine(ResetCharacter());
            GameManager.instance.ContinueWithHeart();
        }

    }
    IEnumerator OnContinueAnim()
    {
        _mJamesAnimator.SetBool("Idle", true);
        _mJamesAnimator.SetBool("GameOver", false);
        yield return new WaitForSeconds(0.3f);
        _mJamesAnimator.SetBool("Won", true);
        _mJamesAnimator.SetBool("Idle", false);

    }
    IEnumerator ResetCharacter()
    {
        yield return new WaitForSeconds(2f);
        _mJamesAnimator.SetBool("Idle", true);
        _mJamesAnimator.SetBool("GameOver", false);

    }
    IEnumerator OnGameOver()
    {
        _mContinue = false;
        _HeartAnimator.SetBool("Revived", false);
        yield return new WaitForSeconds(2f);
        _mJamesAnimator.SetBool("GameOver", true);
        yield return new WaitForSeconds(2f);
        _UIAnimator.SetBool("DisplayPopUp", true);
        _mPopupClosed = false;
        _mHearts.text = GameManager.instance.Player.Hearts.ToString();
        while ((!_mPopupClosed && Timer > 0) && !_mContinue)
        {
            Debug.Log(Timer.ToString());
            Timer--;
            _mTimerTexts.text = Timer.ToString();
            yield return new WaitForSeconds(1f);
        }

        if (_mContinue)
        {
            yield return null;
        }
        else
        {
            OnPanelClicked();
            int amount = GameManager.instance.GainMoney();
            if (amount / 10 >= 1)
                _mCoin.anchoredPosition = new Vector2(103, _mCoin.anchoredPosition.y);
            /*ShopManager.Instance.StartCoroutine(ShopManager.Instance.MoveMoney());*/
            else
                _mCoin.anchoredPosition = new Vector2(137, _mCoin.anchoredPosition.y);
            _UIAnimator.SetBool("EndGame", true);
            _mQuestManager.SetActive(true);

            if (GameManager.instance.Score > GameManager.instance.Player.BestScore)
            {
                _mHighscoreTag.SetActive(true);
                GameManager.instance.Player.UpdateBestScore((int)GameManager.instance.Score);
            }

            _coinsEarned.text = " +" + amount.ToString();
            _bestScore.text = "BEST : " + GameManager.instance.Player.BestScore.ToString();
        }

    }
    public void ResetScreen()
    {
        _UIAnimator.SetBool("EndGame", false);
        _mQuestManager.SetActive(false);
        _mJamesAnimator.SetBool("Idle", true);
        _mJamesAnimator.SetBool("GameOver", false);
        _HeartAnimator.SetInteger("Hearts", 3);
        _mHighscoreTag.SetActive(false);
        Timer = 5;
    }
    public void RestartGame()
    {
        ResetScreen();
        _InputManager.SetActive(false);
        GameManager.instance.OnRestart();
    }

    public void GoHome()
    {
        ResetScreen();
        _InputManager.SetActive(false);
        mySceneManager.instance.LoadHomeScreen();
    }

    private void OnDestroy()
    {
        GameManager.instance.WinScreenHandle -= OnWinScreenDisplay;
    }

}
