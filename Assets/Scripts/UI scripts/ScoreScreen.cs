using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _HeartAnimator;
    [SerializeField] private Image _jamesSprite;
    [SerializeField] private TMP_Text[] _mscoreTexts;
    [SerializeField] private TMP_Text[] _mMnigameCountTexts;
    private void Awake()
    {
        GameManager.instance.WinScreenHandle += OnWinScreenDisplay;
    }


    void OnWinScreenDisplay(bool won, int era, int hearts)
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
        _animator.SetBool("Idle", false);
        _animator.SetBool("Won", won);
        _animator.SetInteger("Era", era + 1); 
        StartCoroutine(Reset());
        if(hearts == 0)
            StartCoroutine(RestartGame());
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(2f); 
        _animator.SetBool("Idle", true);
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(2f);
        mySceneManager.instance.SetScene("LoseScreen", mySceneManager.LoadMode.ADDITIVE);
        _animator.SetInteger("Hearts", 3);
    }

    private void OnDestroy()
    {
        GameManager.instance.WinScreenHandle -= OnWinScreenDisplay;
    }

}
