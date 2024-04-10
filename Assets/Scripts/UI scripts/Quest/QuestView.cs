using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

            var replace = quest.QuestSO.questDescription.Replace("*", quest.Difficulty.amount.ToString());
            var questContainerScript = questContainer.GetComponent<QuestContainer>();
            questContainerScript.QuestDifficulty = quest.Difficulty;
            questContainerScript.Reward.text = (25 * quest.Difficulty.reward).ToString();
            questContainerScript.QuestDescription.text = replace;
            questContainerScript.QuestIcon.sprite = quest.QuestSO.questIcon;
            questContainerScript.QuestColor.color = _difficultyColor[(int)quest.Difficulty.difficulty];
            //StartCoroutine(ShowCompletionBar(questContainerScript.QuestProgression.transform, quest, questContainerScript.StartPosX));

            
            for (var j = 0; j < quest.Difficulty.reward; j++)
            {
                var starObject = questContainerScript.Stars[j].gameObject;
                if (!starObject.activeSelf)
                    starObject.SetActive(true);
            }
        }
    }

    private static IEnumerator ShowCompletionBar(Transform pos, QuestManager.Quest quest, float startPos)
    {
        while (pos.position.x < startPos + 3 * (float)quest.CurrentAmount / quest.MaxAmount)
        {
            pos.position += pos.right * ((float)quest.CurrentAmount / quest.MaxAmount * 3 * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
