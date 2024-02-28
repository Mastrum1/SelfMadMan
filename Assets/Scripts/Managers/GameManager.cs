using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int _mCurrentGame;
    [SerializeField] float _mSpeed;
    [SerializeField] float _mScore;
    [SerializeField] int _mHearts;

    public static GameManager instance;
    MiniGameManager _currentMinigameManager;
    private Scoring _mScoring;

    // Start is called before the first frame update
    void Start()
    {
        _mHearts = 3;
        _mSpeed = 1;

        _mScoring = new Scoring();

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);


        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

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
        if (_mHearts <= 0)
        {
            SceneManager.LoadScene("LoseScreen");
        }
    }

    public void AddCurrent()
    {
        _mCurrentGame++;
    }

    public int GetCurrentGame()
    {
        return _mCurrentGame;
    }

    public float GetScore() 
    {
        return _mScore;
    }

    public string DisplayScore()
    {
        return _mScore.ToString();
    }

    private void HandleMiniGameEnd(bool won, int score)
    {
        if (won == true)
        {
            _mScore = _mScoring.ChangeScore(Scoring.Param.Add, _mScore, score);
            SceneManager.LoadScene("WinScreen");
        }
        else if (won == false) 
        {
            _mScore = _mScoring.ChangeScore(Scoring.Param.Subtract, _mScore, score);
            _mHearts--;
            SceneManager.LoadScene("WinScreen");
        }
    }
}
