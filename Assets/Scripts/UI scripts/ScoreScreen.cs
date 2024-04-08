using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Localization.Platform.Android;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour
{
    [SerializeField] private Animator _mJamesAnimator;
    [SerializeField] private Animator _UIAnimator;
    [SerializeField] private Animator _HeartAnimator;
    [SerializeField] private Image _jamesSprite;
    [SerializeField] private Image _mHighscoreTag;
    [SerializeField] private TMP_Text[] _mscoreTexts;
    [SerializeField] private TMP_Text[] _mMnigameCountTexts;
    [SerializeField] private TMP_Text[] _mTimerTexts;
    [SerializeField] private PanelOnClick _mPanelOnClick;
    [SerializeField] private GameObject _mPopup;

    private int Timer = 5;

    private bool _mPopupClosed = true;
    private bool _mContinue = false;
    private void Awake()
    {

        GameManager.instance.WinScreenHandle += OnWinScreenDisplay;
        _mPanelOnClick.OnClick += OnPanelClicked;
    }


    void OnWinScreenDisplay(bool won, int era, int hearts, bool gameOver)
    {
        Debug.Log(hearts);
        foreach (var text in _mscoreTexts)
        {
            text.text = GameManager.instance.Score.ToString();
        }
        foreach (var text in _mMnigameCountTexts)
        {
            text.text = "GAMES COMPLETED : " + GameManager.instance.MinigameCount.ToString();
        }
        _HeartAnimator.SetInteger("Hearts", hearts);
        _mJamesAnimator.SetBool("Idle", false);
        _mJamesAnimator.SetBool("Won", won);
        _mJamesAnimator.SetInteger("Era", era + 1);


        if (gameOver)
        {
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
    void Countdown()
    {
        if (!_mPopupClosed)
        {

        }

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
            foreach (var text in _mTimerTexts)
            {
                text.text = Timer.ToString();
            }
            yield return new WaitForSeconds(1f);
        }

        if (_mContinue)
        {
            //TO DO;
        }
        else
        {
            _UIAnimator.SetBool("EndGame", true);
            if(GameManager.instance.Score > Player.insta)
        }
           

    }

    private void OnDestroy()
    {
        GameManager.instance.WinScreenHandle -= OnWinScreenDisplay;
    }

}
