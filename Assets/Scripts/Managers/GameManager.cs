using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] int _mCurrentGame;
    [SerializeField] float _mSpeed;
    [SerializeField] float _mScore;
    [SerializeField] int _mHearts;

    MiniGameManager _currentMinigameManager;
    private Scoring _mScoring;

    // Start is called before the first frame update
    void Start()
    {
        _mHearts = 1;
        _mSpeed = 1;

        _mScoring = new Scoring();

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame

    public void SelectNewMiniGame(MiniGameManager myMinigame)
    {
        if(_currentMinigameManager !=null)
        {
            _currentMinigameManager.OnMiniGameEnd -= HandleMiniGameEnd;
        }
        _currentMinigameManager = myMinigame;
        _currentMinigameManager.OnMiniGameEnd += HandleMiniGameEnd;
    }
    void Update()
    {
        
    }

    public void AddCurrent()
    {
        _mCurrentGame++;
    }

    public int GetCurrentGame()
    {
        return _mCurrentGame;
    }

    public void ResetCurrentGame()
    {
        _mCurrentGame = 0;
        _mHearts = 3;
    }

    public float GetScore() 
    {
        return _mScore;
    }

    public string DisplayScore()
    {
        return _mScore.ToString();
    }

    public void Retry()
    {

    }

    private void HandleMiniGameEnd(bool won, int score)
    {
        if (won == true)
        {
            _mScore = _mScoring.ChangeScore(Scoring.Param.Add, _mScore, score);
            _mHearts--;

            if (_mHearts <= 0)
            {
                SceneManager.LoadScene("LoseScreen");
            }
            else
            {
                SceneManager.LoadScene("WinScreen");
            }
        }
        else if (won == false) 
        {
            _mScore = _mScoring.ChangeScore(Scoring.Param.Subtract, _mScore, score);
            _mHearts--;
            SceneManager.LoadScene("WinScreen");

            if (_mHearts <= 0)
            {
                SceneManager.LoadScene("LoseScreen");
            }
            else
            {
                SceneManager.LoadScene("WinScreen");
            }
        }
    }
}
