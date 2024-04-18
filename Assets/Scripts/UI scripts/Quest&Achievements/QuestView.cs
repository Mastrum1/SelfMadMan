using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestView : MonoBehaviour
{
    private QuestManager _mQuestManager;

    [SerializeField] private List<GameObject> _quests;
    [SerializeField] private List<Color> _difficultyColor;

    private void OnEnable()
    {
        _mQuestManager = QuestManager.Instance;
        _mQuestManager.OnUpdateQuestUI += UpdateQuestUI;
        
        LoadQuestContainers();
    }

    private void LoadQuestContainers()
    {
        var questCount = _mQuestManager.SelectedQuests.Count;
        for (var i = 0; i < questCount; i++)
        {
            UpdateQuestUI(i, null);
        }
    }

    private void UpdateQuestUI(int container, int? questNum)
    {
        var quest = questNum == null ? _mQuestManager.SelectedQuests[container] : _mQuestManager.SelectedQuests[(int)questNum];
        
        var questContainer = _quests[container];
        questContainer.SetActive(true);

        var replace = quest.QuestSO.questDescription.Replace("*", quest.Difficulty.amount.ToString());
        var questContainerScript = questContainer.GetComponent<QuestContainer>();
        questContainerScript.SelectedQuest = quest;
        questContainerScript.QuestDifficulty = quest.Difficulty;
        questContainerScript.Reward.text = (25 * quest.Difficulty.reward).ToString();
        questContainerScript.QuestDescription.text = replace;
        questContainerScript.QuestIcon.sprite = quest.QuestSO.questIcon;
        questContainerScript.QuestColor.color = _difficultyColor[(int)quest.Difficulty.difficulty];
        float amount;
        if (quest.CurrentAmount >= quest.MaxAmount)
        {
            amount = 1;
        }
        else
        {
            amount = (float)quest.CurrentAmount / quest.MaxAmount;
        }
        StartCoroutine(ShowCompletionBar(questContainerScript.QuestProgression.gameObject.transform, amount, questContainerScript.StartPosX));

            
        for (var j = 0; j < quest.Difficulty.reward; j++)
        {
            var starObject = questContainerScript.Stars[j].gameObject;
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

    public void IsInQuestMenu()
    {
        foreach (var quest in _quests)
        {
            quest.GetComponent<QuestContainer>().IsInQuestMenu = true;
        }
    }

    private void OnDisable()
    {
        foreach (var quest in _quests)
        {
            quest.SetActive(false);
            quest.GetComponent<QuestContainer>().ResetFillBar();
        }
    }
}
