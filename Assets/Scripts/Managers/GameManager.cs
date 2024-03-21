using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int Level { get => _mLevelCount; private set => _mLevelCount = value; }
    public float Speed { get; private set; }
    private int _era;
    public int Era { get => _era - 1;  set => _era = value; }

    [SerializeField] float _mScore;
    [SerializeField] int _mHearts;

    MiniGameManager _currentMinigameManager;
    private Scoring _mScoring;
    private QuestManager _mQuestManager;

    private int _mMinigameCount;
    private int _mLevelCount;
    private int _mCurrentStars;

    private void Awake()
    {

        Application.targetFrameRate = 60;

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

        _mMinigameCount = 0;
        Era = 1;
        _mHearts = 3;
        Speed = 10;

        _mQuestManager = QuestManager.instance;
        _mQuestManager.OnQuestComplete += AddStars;
        _mScoring = new Scoring();
    }

    public void OnGameStart()
    {
        mySceneManager.instance.LoadWinScreen();
        mySceneManager.instance.RandomGameChoice();
    }

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
        Debug.Log("Faster");
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

        mySceneManager.instance.UnloadCurrentScene();

        _mHearts -= won ? 0 : 1;
        score += won ? 100 : 0;
        _mScore = _mScoring.ChangeScore(Scoring.Param.Add, _mScore, score);

        if (_mHearts <= 0)
        {
            SceneManager.LoadScene("LoseScreen");
            ResetGame();
        }
        else
            StartCoroutine(ContinueMinigames());


    }

    IEnumerator ContinueMinigames()
    {
        if (_mMinigameCount % 3 == 0)
        {
            Faster();

        }
        yield return new WaitForSeconds(3f);
        mySceneManager.instance.RandomGameChoice();

    }

    void AddStars(int reward)
    {
        _mCurrentStars += reward;
        if (_mCurrentStars >= 5)
        {
            Level++;
            _mCurrentStars -= 5;
        }
    }

    private void OnDestroy()
    {
        //_mQuestManager.OnQuestComplete -= AddStars; => A fix Jimmy
    }
}
