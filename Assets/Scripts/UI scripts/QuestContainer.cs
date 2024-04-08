using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestContainer : MonoBehaviour
{
    public Quests Quest { get => _mQuest; set => _mQuest = value; }
    [SerializeField] private Quests _mQuest;

    public Quests.Difficulty QuestDifficulty { get => _mQuestDifficulty; set => _mQuestDifficulty = value; }
    [SerializeField] private Quests.Difficulty _mQuestDifficulty;

    public TMP_Text QuestName { get => _mQuestName; set => _mQuestName = value; }
    [SerializeField] private TMP_Text _mQuestName;

    public TMP_Text QuestDescription { get => _mQuestDescription; set => _mQuestDescription = value; }
    [SerializeField] private TMP_Text _mQuestDescription;

    public Image QuestStars { get => _mQuestStars; set => _mQuestStars = value; }
    [SerializeField] private Image _mQuestStars;

    public Image Reward { get => _mReward; set => _mReward = value; }
    [SerializeField] private Image _mReward;

    public Image QuestIcon { get => _mQuestIcon; set => _mQuestIcon = value; }
    [SerializeField] private Image _mQuestIcon;

    public Slider QuestProgression { get => _mQuestProgression; set => _mQuestProgression = value; }
    [SerializeField] private Slider _mQuestProgression;


    private void Start()
    {
        _mQuestName.text = _mQuest.questName;
        _mQuestDifficulty = _mQuest.difficulties[0];
        _mQuestStars.fillAmount = _mQuestDifficulty.amount / 100f;
        _mReward.fillAmount = _mQuestDifficulty.reward / 100f;
        _mQuestDescription.text = _mQuest.questDescription;
    }
}
