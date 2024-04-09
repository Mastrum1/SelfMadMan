using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestView : MonoBehaviour
{
    private QuestManager _mQuestManager;

    [SerializeField] private List<GameObject> _quests;
    private void OnEnable()
    {
        _mQuestManager = QuestManager.instance;

        LoadQuestContainer();
    }
    
    private void LoadQuestContainer()
    {
        var questCount = _mQuestManager.SelectedQuests.Count;
        for (var i = 0; i < questCount; i++)
        {
            var quest = _mQuestManager.SelectedQuests[i];
            var questContainer = _quests[i];
            questContainer.SetActive(true);

            var questContainerScript = questContainer.GetComponent<QuestContainer>();
            questContainerScript.QuestDifficulty = quest.Difficulty;
            questContainerScript.Reward.text = quest.Difficulty.reward.ToString();
            questContainerScript.QuestDescription.text = quest.QuestSO.questDescription;
            questContainerScript.QuestIcon.sprite = Resources.Load<Sprite>(quest.Sprite);
            questContainerScript.QuestProgression.fillAmount = (float)quest.CurrentAmount / quest.MaxAmount;

            var difficultyValue = (int)quest.Difficulty.difficulty;
            for (var j = 0; j <= difficultyValue; j++)
            {
                var starObject = questContainerScript.Stars[j].gameObject;
                if (!starObject.activeSelf)
                    starObject.SetActive(true);
            }
        }
    }
}
