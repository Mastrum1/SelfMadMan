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
    [SerializeField] private GameObject _mFillBarStartPos;
    [SerializeField] private GameObject _mFillBarEndPos;
    [SerializeField] private List<Color> _difficultyColor;
    [SerializeField] private List<GameObject> _mStars;
    
    public bool IsInQuestMenu { set => _mIsInQuestMenu = value; }
    private bool _mIsInQuestMenu;

    private Vector3 _mMaxDistance;

    private void Start()
    {
        _mMaxDistance = _mFillBarEndPos.transform.position - _mFillBarStartPos.transform.position;
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
        if(_mIsInQuestMenu)
        {
            StartCompletion();
        }
            
        for (var j = 0; j < _mSelectedQuest.Difficulty.reward; j++)
        {
            var starObject = _mStars[j].gameObject;
            if (!starObject.activeSelf)
                starObject.SetActive(true);
        }
    }

    public void StartCompletion()
    {
        float amount;
        if (_mSelectedQuest.CurrentAmount >= _mSelectedQuest.MaxAmount)
        {
            amount = 1;
        }
        else
        {
            amount = (float)_mSelectedQuest.CurrentAmount / _mSelectedQuest.MaxAmount;
        }
        StartCoroutine(ShowCompletionBar(_mQuestProgression.gameObject.transform, amount, _mFillBarStartPos.transform.position.x, _mMaxDistance.x));

    }
    private static IEnumerator ShowCompletionBar(Transform pos, float amount, float startPos, float maxDistance)
    {
        while (pos.position.x < startPos + maxDistance * amount)
        {
            pos.position += pos.right * (amount * maxDistance * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void ChangeQuest(int container)
    {
        if (!MoneyManager.Instance.SubtractMoney(25 * _mSelectedQuest.Difficulty.reward)) return;
        
        gameObject.SetActive(false);
        QuestManager.Instance.OnChangeQuest(_mSelectedQuest, container);
    }

    public void ResetFillBar()
    {
        _mQuestProgression.transform.position = _mFillBarStartPos.transform.position;
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
