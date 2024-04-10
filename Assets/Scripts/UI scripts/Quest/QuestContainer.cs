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

    public GameObject QuestProgression { get => _mQuestProgression; set => _mQuestProgression = value; }
    [SerializeField] private GameObject _mQuestProgression;
    
    public float StartPosX => _mStartPos;
    private float _mStartPos;
    
    public List<GameObject> Stars { get => _mStars; set => _mStars = value; }
    [SerializeField] private List<GameObject> _mStars;

    private void Awake()
    {
        _mStartPos = QuestProgression.transform.position.x;
    }

    private void OnDisable()
    {
        QuestProgression.transform.position = new Vector3(_mStartPos, transform.position.y);
    }
}
