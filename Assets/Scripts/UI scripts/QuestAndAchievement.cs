using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestAndAchievement : MonoBehaviour
{
    private QuestManager _mQuestManager;

    [SerializeField] private GameObject _mQuestView;

    private void OnEnable()
    {
        _mQuestManager = QuestManager.instance;

        LoadQuestContainer();
    }

    private void LoadQuestContainer()
    {
        Debug.Log("load quests");
        var childCount = 0;

        foreach (var quest in _mQuestManager.SelectedQuests)
        {
          
           var questContainer = _mQuestView.transform.GetChild(childCount).gameObject;
           questContainer.SetActive(true);
           var questContainerScript = questContainer.GetComponent<QuestContainer>();
           questContainerScript.QuestDifficulty = quest.Difficulty;
           questContainerScript.Reward.text = quest.Difficulty.reward.ToString();
           questContainerScript.QuestDescription.text = quest.QuestSO.questDescription;
           questContainerScript.QuestIcon.sprite = quest.QuestSO.questIcon;
           questContainerScript.QuestProgression.fillAmount = (float)quest.CurrentAmount / quest.MaxAmount;
           
           Debug.Log((int)quest.Difficulty.difficulty);

           for (int i = 0; i <= (int)quest.Difficulty.difficulty; i++)
           {
                if (!questContainerScript.StarsContainer.transform.GetChild(i).gameObject.activeSelf)
                    questContainerScript.StarsContainer.transform.GetChild(i).gameObject.SetActive(true);
           }

           childCount++;
        }
    }
}
