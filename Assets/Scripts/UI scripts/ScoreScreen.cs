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
    [SerializeField] private TMP_Text[] _mscoreTexts;
    [SerializeField] private TMP_Text[] _mMnigameCountTexts;
    [SerializeField] private TMP_Text[] _mTimerTexts;
    [SerializeField] private PanelOnClick _mPanelOnClick;
    [SerializeField] private GameObject _mPopup;

    private int Timer = 5;

    private bool _mPopupClosed;
    private bool _mContinue = false;
    private void Awake()
    {
        GameManager.instance.WinScreenHandle += OnWinScreenDisplay;
        _mPanelOnClick.OnClick += OnPanelClicked;
    }


    void OnWinScreenDisplay(bool won, int era, int hearts, bool gameOver)
    {
        Debug.Log(hearts);
        foreach(var text in _mscoreTexts)
        {
            text.text = GameManager.instance.DisplayScore();
        }
        foreach (var text in _mMnigameCountTexts)
        {
            text.text = "GAMES COMPLETED : " + GameManager.instance.MinigameCount.ToString();
        }
        _HeartAnimator.SetInteger("Hearts", hearts);
        _mJamesAnimator.SetBool("Idle", false);
        _mJamesAnimator.SetBool("Won", won);
        _mJamesAnimator.SetInteger("Era", era + 1);


        if(gameOver)
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
    }
    IEnumerator Countdown() 
    {
        if( !_mPopupClosed )
        {
            Timer--;
            foreach(var text in _mTimerTexts )
            {
                text.text = Timer.ToString();
            }
        }
        yield return new WaitForSeconds(1f);
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
        _mPopup.SetActive(true);
        do
        {
            StartCoroutine(Countdown());
        } while (!_mPopupClosed || Timer == 0);

        if(_mContinue)
        {
            //TO DO;
        }
        else
        {

        }
    }

    private void OnDestroy()
    {
        GameManager.instance.WinScreenHandle -= OnWinScreenDisplay;
    }

}
