using System.Collections.Generic;
using UnityEngine;

public class QuestView : MonoBehaviour
{
    private QuestManager _mQuestManager;

    [SerializeField] private List<GameObject> _quests;
    [SerializeField] private List<Color> _difficultyColor;

    private void OnEnable()
    {
        _mQuestManager = QuestManager.instance;
        
        LoadQuestContainer();
    }

    private void LoadQuestContainer()
    {
        var questCount = _mQuestManager.SelectedQuests.Count;
        Debug.Log(_mQuestManager.SelectedQuests.Count);
        for (var i = 0; i < questCount; i++)
        {
            
            var quest = _mQuestManager.SelectedQuests[i];
            var questContainer = _quests[i];
            questContainer.SetActive(true);

            var questContainerScript = questContainer.GetComponent<QuestContainer>();
            questContainerScript.QuestDifficulty = quest.Difficulty;
            questContainerScript.Reward.text = (25 * quest.Difficulty.reward).ToString();
            questContainerScript.QuestDescription.text = quest.QuestSO.questDescription;
            questContainerScript.QuestIcon.sprite = quest.QuestSO.questIcon;
            questContainerScript.QuestColor.color = _difficultyColor[(int)quest.Difficulty.difficulty];
            questContainerScript.QuestProgression.fillAmount = (float)quest.CurrentAmount / quest.MaxAmount;
            
            for (var j = 0; j < quest.Difficulty.reward; j++)
            {
                var starObject = questContainerScript.Stars[j].gameObject;
                if (!starObject.activeSelf)
                    starObject.SetActive(true);
            }
        }
    }
}
