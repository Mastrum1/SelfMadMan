using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestContainer : MonoBehaviour
{
    public QuestManager.Quest SelectedQuest { get => _mSelectedQuest; set => _mSelectedQuest = value; }
    [SerializeField] private QuestManager.Quest _mSelectedQuest;
    
    [SerializeField] private TMP_Text _mReward;
    [SerializeField] private TMP_Text _mQuestDescription;
    [SerializeField] private Image _mQuestIcon;
    [SerializeField] private Image _mQuestColor;
    [SerializeField] private GameObject _mQuestProgression;
    [SerializeField] private GameObject _mChangeButton;

    public bool IsInQuestMenu { set => _mIsInQuestMenu = value; }
    private bool _mIsInQuestMenu;
    
    [SerializeField] private List<Color> _difficultyColor;
    
    private float _mStartPos;
    
    [SerializeField] private List<GameObject> _mStars;

    private void Awake()
    {
        _mStartPos = _mQuestProgression.transform.position.x;
    }

    private void Start()
    {
        _mChangeButton.SetActive(_mIsInQuestMenu);
    }
    
    public void UpdateQuestUI()
    {
        if(!gameObject.activeSelf) gameObject.SetActive(true);
        
        ResetFillBar();
        var replace = _mSelectedQuest.QuestSO.questDescription.Replace("*", _mSelectedQuest.Difficulty.amount.ToString());
        _mReward.text = (25 * _mSelectedQuest.Difficulty.reward).ToString();
        _mQuestDescription.text = replace;
        _mQuestIcon.sprite = _mSelectedQuest.QuestSO.questIcon;
        _mQuestColor.color = _difficultyColor[(int)_mSelectedQuest.Difficulty.difficulty];
        float amount;
        if (_mSelectedQuest.CurrentAmount >= _mSelectedQuest.MaxAmount)
        {
            amount = 1;
        }
        else
        {
            amount = (float)_mSelectedQuest.CurrentAmount / _mSelectedQuest.MaxAmount;
        }
        StartCoroutine(ShowCompletionBar(_mQuestProgression.gameObject.transform, amount, _mStartPos));

            
        for (var j = 0; j < _mSelectedQuest.Difficulty.reward; j++)
        {
            var starObject = _mStars[j].gameObject;
            if (!starObject.activeSelf)
                starObject.SetActive(true);
        }
    }

    private static IEnumerator ShowCompletionBar(Transform pos, float amount, float startPos)
    {
        while (pos.position.x < startPos + 3 * amount)
        {
            pos.position += pos.right * (amount * 3 * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void ChangeQuest(int container)
    {
        gameObject.SetActive(false);
        QuestManager.Instance.OnChangeQuest(_mSelectedQuest, container);
    }

    public void ResetFillBar()
    {
        _mQuestProgression.transform.position = new Vector3(_mStartPos - 0.176f, transform.position.y);
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
        Debug.Log("Container Destroyed");
        _mIsInQuestMenu = false;
    }
}
