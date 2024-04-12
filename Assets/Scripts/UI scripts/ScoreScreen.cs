using CW.Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private TMP_Text _mMnigameCountTexts;
    [SerializeField] private TMP_Text _mTimerTexts;
    [SerializeField] private TMP_Text _bestScore;
    [SerializeField] private PanelOnClick _mPanelOnClick;
    [SerializeField] private GameObject _mPopup;
    [SerializeField] private GameObject _InputManager;
    [SerializeField] private GameObject _mQuestManager;

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
        _mMnigameCountTexts.text = "GAMES COMPLETED : " + GameManager.instance.MinigameCount.ToString();
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

    IEnumerator ResetCharacter()
    {
        yield return new WaitForSeconds(2f);
        _mJamesAnimator.SetBool("Idle", true);
    }
    IEnumerator OnGameOver()
    {
        yield return new WaitForSeconds(2f);
        _mJamesAnimator.SetBool("GameOver", true);
        yield return new WaitForSeconds(2f);
        _UIAnimator.SetBool("DisplayPopUp", true);
        _mPopupClosed = false;

        while (!_mPopupClosed && Timer > 0)
        {
            Debug.Log(Timer.ToString());
            Timer--;
            _mTimerTexts.text = Timer.ToString();
            yield return new WaitForSeconds(1f);
        }

        if (_mContinue)
        {
            _InputManager.SetActive(false);
            //TO DO;
        }
        else
        {
            OnPanelClicked();
            _UIAnimator.SetBool("EndGame", true);
            _mQuestManager.SetActive(true);

            if (GameManager.instance.Score > GameManager.instance.Player.BestScore)
            {
                _mHighscoreTag.SetActive(true);
                GameManager.instance.Player.UpdateBestScore((int)GameManager.instance.Score);
            }
            _bestScore.text = "BEST : " + GameManager.instance.Player.BestScore.ToString();

        }
    }
    public void ResetScreen()
    {
        _UIAnimator.SetBool("EndGame", false);
        //_mQuestManager.SetActive(false);
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
