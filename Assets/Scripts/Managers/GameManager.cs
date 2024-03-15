using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public float Speed { get; private set; }
    [SerializeField] float _mScore;
    [SerializeField] int _mHearts;

    MiniGameManager _currentMinigameManager;
    private Scoring _mScoring;

    private int _mMinigameCount;

    // Start is called before the first frame update
    void Start()
    {
        _mMinigameCount = 0;
        Application.targetFrameRate = 60;
        _mHearts = 3;
        Speed = 10;

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
        _mMinigameCount++;
        if (_currentMinigameManager != null)
        {
            _currentMinigameManager.OnMiniGameEnd -= HandleMiniGameEnd;
        }
        _currentMinigameManager = myMinigame;
        _currentMinigameManager.OnMiniGameEnd += HandleMiniGameEnd;

    }

    private void Faster()
    {
        Speed *= 0.8f;
    }
    public void ResetGame()
    {
        _mScore = 0;
        _mHearts = 3;
        Speed = 10;
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

    private void HandleMiniGameEnd(bool won, int score)
    {
        Debug.Log("is finished");
       
        if(_mMinigameCount % 3 == 0)
            Faster();
        
        _mHearts -= won ? 0 : 1;
        score += won ? 100 : 0;
        _mScore = _mScoring.ChangeScore(Scoring.Param.Add, _mScore, score);
        if (_mHearts <= 0)
        {
            SceneManager.LoadScene("LoseScreen");
            ResetGame();
        }
        else
            SceneManager.LoadScene("WinScreen");
    }
}
