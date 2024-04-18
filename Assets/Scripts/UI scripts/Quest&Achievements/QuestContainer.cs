using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestContainer : MonoBehaviour
{
    public QuestManager.Quest SelectedQuest { get => _mSelectedQuest; set => _mSelectedQuest = value; }
    [SerializeField] private QuestManager.Quest _mSelectedQuest;
    
    public Quests.Difficulty QuestDifficulty { set => _mQuestDifficulty = value; }
    [SerializeField] private Quests.Difficulty _mQuestDifficulty;

    public TMP_Text Reward => _mReward;
    [SerializeField] private TMP_Text _mReward;

    public TMP_Text QuestDescription => _mQuestDescription;
    [SerializeField] private TMP_Text _mQuestDescription;

    public Image QuestIcon => _mQuestIcon; 
    [SerializeField] private Image _mQuestIcon;
    
    public Image QuestColor => _mQuestColor;
    [SerializeField] private Image _mQuestColor;

    public GameObject QuestProgression => _mQuestProgression;
    [SerializeField] private GameObject _mQuestProgression;
    
    [SerializeField] private GameObject _mChangeButton;

    public bool IsInQuestMenu { get => _mIsInQuestMenu; set => _mIsInQuestMenu = value; }
    private bool _mIsInQuestMenu;
    
    public float StartPosX => _mStartPos;
    private float _mStartPos;
    
    public List<GameObject> Stars => _mStars;
    [SerializeField] private List<GameObject> _mStars;

    private void Awake()
    {
        _mStartPos = QuestProgression.transform.position.x;
    }

    private void Start()
    {
        _mChangeButton.SetActive(_mIsInQuestMenu);
    }

    public void ChangeQuest(int container)
    {
        gameObject.SetActive(false);
        QuestManager.Instance.OnChangeQuest(_mSelectedQuest, container);
    }

    public void ResetFillBar()
    {
        QuestProgression.transform.position = new Vector3(_mStartPos - 0.176f, transform.position.y);
    }

    private void OnDisable()
    {
        foreach (var star in _mStars)
        {
            star.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        _mIsInQuestMenu = false;
    }
}
