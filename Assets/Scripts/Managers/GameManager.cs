using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public event Action OnUpdateLevel;

    private float _mLostFocusTime;

    private float _speed;
    public float Speed { get => _speed; private set => _speed = value; }

    private int _era;
    public int Era { get => _era - 1; set => _era = value; }

    private int _fasterLevel;
    public int FasterLevel { get => _fasterLevel; set => _fasterLevel = value; }

    [SerializeField] private Player _mPlayer;
    public Player Player { get => _mPlayer; }

    [SerializeField] private MoneyManager _mMoneyManager;
    public MoneyManager MoneyManager { get => _mMoneyManager; }

    public class EraData
    {
        public bool Unlocked;
        public int _price;
        public EraData(bool unlocked, int price)
        {
            Unlocked = unlocked;
            _price = price;
        }
        public void UnlockEra()
        {
            Unlocked = true;
        }
    }
    private List<EraData> _mErasData = new List<EraData>();
    public List<EraData> ErasData { get => _mErasData; private set => _mErasData = value; }

    private float _mScore;
    public float Score { get => _mScore; private set => _mScore = value; }
    public int _mHearts;

    private bool _mGameOver;

    [Scene] public string FasterScene;

    private MiniGameManager _currentMinigameManager;
    private Scoring _mScoring;
    private QuestManager _mQuestManager;

    private int _mMinigameCount;
    private int _mMinigameWon;
    public int MinigamesWon { get => _mMinigameWon; private set => _mMinigameWon = value; }

    public string[] PlayerTitle => _mPlayerTitle;
    [SerializeField] private string[] _mPlayerTitle;

    public event Action<bool, int, int, bool> WinScreenHandle;

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
        //NSUserTrackingUsageDescription
        _mGameOver = false;
        _mMinigameCount = 0;
        _mMinigameWon = 0;
        Era = 1;
        _mHearts = 3;
        Speed = 10;
        FasterLevel = 1;
        _mScore = 0;

        QuestManager.Instance.OnReward += AddStars;
        _mPlayer.LoadJson();
        _mScoring = new Scoring();
    }

    public void ResetGame()
    {
        _mScore = 0;
        _mHearts = 3;
        Speed = 10;
        FasterLevel = 1;
        _mMinigameCount = 0;
        _mGameOver = false;
        _mMinigameWon = 0;
    }

    public void LoadEraData(List<EraData> data)
    {
        _mErasData = data;
    }

    public void OnGameStart()
    {
        if (_mErasData[Era].Unlocked)
        {
            ResetGame();
            mySceneManager.instance.LoadWinScreen();
            mySceneManager.instance.RandomGameChoice();
        }
    }
    public void OnRestart()
    {
        ResetGame();
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
        _mGameOver = _mHearts == 0 ? true : false;
        MinigamesWon += won ? 1 : 0;
        score += won ? 100 : 0;
        _mScore = _mScoring.ChangeScore(Scoring.Param.Add, _mScore, score);
        WinScreenHandle?.Invoke(won, Era, _mHearts, _mGameOver);
        if (!_mGameOver)
            StartCoroutine(ContinueMinigames());
    }
    public void ContinueWithHeart()
    {
        _mGameOver = false;
        _mHearts++;
        _mMinigameCount = 1;
        StartCoroutine(ContinueMinigames());
    }
    public IEnumerator ContinueMinigames()
    {
        yield return new WaitForSeconds(2f);
        if (_mMinigameCount % 3 == 0)
        {
            Faster();
            yield return new WaitForSeconds(2f);
        }
        mySceneManager.instance.RandomGameChoice();
    }
    public int GainMoney()
    {
        float amount = Score % 100 == 0 ? Score / 100 : (Score / 100) + 1;
        Player.NewCurrency(Player.Money + (int)amount);
        return (int)amount;
    }

    private void AddStars(int reward)
    {
        Player.AddStars(reward);
        if (Player.Xp >= 5)
        {
            Player.LevelUp();
        }
        OnUpdateLevel?.Invoke();
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            _mPlayer.SaveJson();
            _mLostFocusTime = System.DateTime.Now.Minute;
        }
        if (hasFocus)
        {
            Debug.Log(_mLostFocusTime);
            if (System.DateTime.Now.Minute - _mLostFocusTime > 2)
            {
                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    Debug.Log(SceneManager.GetSceneAt(i));
                    SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
                }
                SceneManager.LoadSceneAsync("LoadingScreen");

            }
        }
    }

    void OnApplicationQuit()
    {
        _mPlayer.SaveJson();

    }
    private void OnDestroy()
    {
        // _mQuestManager.OnReward -= AddStars;

    }
}
