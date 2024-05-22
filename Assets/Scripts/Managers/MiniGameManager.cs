using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using NaughtyAttributes;

public class MiniGameManager : MonoBehaviour
{
    public delegate void MiniGameEndHandler(bool won, int score);
    public event MiniGameEndHandler OnMiniGameEnd;


    [SerializeField] [Scene] private string _scenName;
    protected int Amount { get; set; }

    protected bool _gameIsPlaying;

    [SerializeField] public Timer _mTimer;
    [SerializeField] private GameObject _cashGO;
    [SerializeField] private Animator _cash;
    [SerializeField] private GameObject _loosePanel;

    [NonSerialized] public int miniGameScore;

    private QuestManager _questManager;

    public virtual void Awake()
    {
        Amount = 0;
        _gameIsPlaying = true;
        _mTimer.ResetTimer(GameManager.instance.Speed);
        GameManager.instance.SelectNewMiniGame(this);
        _questManager = QuestManager.Instance;
    }

    protected virtual void EndMiniGame(bool won, int score)
    {
        Debug.Log("ENDGAME");

        _gameIsPlaying = false;
        _mTimer.MyTimer = false;

        if (won)
        {
            _cashGO.SetActive(true);
            _cash.SetBool("Won", true);
            TrophyManager.Instance.AddTrophyAmount(6,1);
        }
        else
            _loosePanel.SetActive(true);

        float timeout = won ? 1.5f : 0.5f;
        
        foreach (var quest in _questManager.SelectedQuests.Where(quest => quest.QuestSO.scene.SceneName == _scenName).ToArray())
        {
            quest.CurrentAmount += Amount;
            _questManager.CheckQuestCompletion(quest);
        }

        StartCoroutine(StartAnim(won, score, timeout));
    }

    IEnumerator StartAnim(bool won, int score, float timeout)
    {
        yield return new WaitForSeconds(timeout);

        if (_loosePanel.activeSelf)
            _loosePanel.SetActive(false);

        OnMiniGameEnd?.Invoke(won, score);
    }

    public virtual void Update()
    {
        if (_mTimer.TimerValue == 0 && _gameIsPlaying)
            EndMiniGame(false, 0);
    }
}