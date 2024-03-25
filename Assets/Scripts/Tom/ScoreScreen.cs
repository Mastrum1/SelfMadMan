using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _jamesSprite;
    [SerializeField] private Sprite[] _jamesSprites;
    private void Awake()
    {
        GameManager.instance.WinScreenHandle += OnWinScreenDisplay;
    }


    void OnWinScreenDisplay(bool won, int era)
    {
        _jamesSprite.overrideSprite = _jamesSprites[GameManager.instance.Era];
        _animator.SetBool("Idle", false);
        _animator.SetBool("Won", won);
        _animator.SetInteger("Era", era + 1); 
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(2f); 
        _animator.SetBool("Idle", true);
    }
    private void OnDestroy()
    {
        GameManager.instance.WinScreenHandle -= OnWinScreenDisplay;
    }

}
