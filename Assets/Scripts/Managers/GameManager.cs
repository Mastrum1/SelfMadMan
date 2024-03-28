using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using NaughtyAttributes;
using Unity.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int Level { get => _mLevelCount; private set => _mLevelCount = value; }

    private float _speed;
    public float Speed { get => _speed; private set => _speed = value; }

    private int _era;
    public int Era { get => _era - 1; set => _era = value; }

    private int _fasterLevel;
    public int FasterLevel { get => _fasterLevel; set => _fasterLevel = value; }

    private Dictionary<int,bool> _unlockedEra = new Dictionary<int,bool>();


    [SerializeField] float _mScore;
    [SerializeField] int _mHearts;

    [Scene] public string FasterScene; 

    MiniGameManager _currentMinigameManager;
    private Scoring _mScoring;
    private QuestManager _mQuestManager;

    private int _mMinigameCount;
    private int _mLevelCount;
    private int _mCurrentStars;

    public event Action<bool, int, int> WinScreenHandle;

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
        FasterLevel = 1;

        _unlockedEra.Add(0, true);

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

        mySceneManager.instance.SetScene(FasterScene, mySceneManager.LoadMode.ADDITIVE);
        FasterLevel++;
        Speed *= 0.8f;


    }
    public void ResetGame()
    {
        _mScore = 0;
        _mHearts = 3;
        Speed = 10;
        FasterLevel = 1;
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
        WinScreenHandle?.Invoke(won, Era, _mHearts);
        if (_mHearts > 0)
        {
            StartCoroutine(ContinueMinigames(won));
        }
    }

    IEnumerator ContinueMinigames(bool won)
    {

        yield return new WaitForSeconds(2f);
        if (_mMinigameCount % 3 == 0)
        {
            Faster();
            yield return new WaitForSeconds(2f);
        }
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
