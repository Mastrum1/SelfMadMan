using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestContainer : MonoBehaviour
{
    public Quests.Difficulty QuestDifficulty { get => _mQuestDifficulty; set => _mQuestDifficulty = value; }
    [SerializeField] private Quests.Difficulty _mQuestDifficulty;

    public TMP_Text Reward { get => _mReward; set => _mReward = value; }
    [SerializeField] private TMP_Text _mReward;

    public TMP_Text QuestDescription { get => _mQuestDescription; set => _mQuestDescription = value; }
    [SerializeField] private TMP_Text _mQuestDescription;

    public Image QuestIcon { get => _mQuestIcon; set => _mQuestIcon = value; }
    [SerializeField] private Image _mQuestIcon;
    
    public Image QuestColor { get => _mQuestColor; set => _mQuestColor = value; }
    [SerializeField] private Image _mQuestColor;

    public Image QuestProgression { get => _mQuestProgression; set => _mQuestProgression = value; }
    [SerializeField] private Image _mQuestProgression;
    
    public List<GameObject> Stars { get => _mStars; set => _mStars = value; }
    [SerializeField] private List<GameObject> _mStars;
}
