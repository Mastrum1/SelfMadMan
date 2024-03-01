using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] string _mCurrentGame;
    [SerializeField] float _mSpeed;
    [SerializeField] float _mScore;
    [SerializeField] int _mHearts;

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

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame

    public void SelectNewMiniGame(MiniGameManager myMinigame)
    {
        if (_currentMinigameManager != null)
        {
            _currentMinigameManager.OnMiniGameEnd -= HandleMiniGameEnd;
        }
        _currentMinigameManager = myMinigame;
        _currentMinigameManager.OnMiniGameEnd += HandleMiniGameEnd;

    }

    public string GetCurrentGame()
    {
        return _mCurrentGame;
    }

    public void ResetGame()
    {
        _mCurrentGame = null;
        _mScore = 0;
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

    public int GetHearts()
    {
        return _mHearts;
    }

    public float GetSpeed()
    {
        return _mSpeed;
    }

    private void HandleMiniGameEnd(bool won, int score)
    {
        Debug.Log("is finished");
       
        _mHearts -= won ? 0 : 1;
        score += won ? 100 : 0;
        _mScore = _mScoring.ChangeScore(Scoring.Param.Add, _mScore, score);
        if (_mHearts <= 0)
        {
            SceneManager.LoadScene("LoseScreen");
            _mHearts = 3;
        }
        else
            SceneManager.LoadScene("WinScreen");
    }
}
