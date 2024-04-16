using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TrophyView : MonoBehaviour
{
    private TrophyManager _mTrophyManager;

    [SerializeField] private List<GameObject> _trophies;
    [SerializeField] private TMP_Text _currentAmount;
    [SerializeField] private TMP_Text _maxAmount;
    
    private void OnEnable()
    {
        _mTrophyManager = TrophyManager.Instance;
        
        SetAmounts();
        LoadTrophyContainers();
    }

    private void LoadTrophyContainers()
    {
        for (var i = 0; i < _mTrophyManager.TrophyList.Count; i++)
        {
            if (i > _trophies.Count)
            {
                Debug.LogError("The trophy list > the trophyContainer list");
                return;
            }
            
            var trophy = _mTrophyManager.TrophyList[i];
            var trophyContainer = _trophies[i];
            trophyContainer.SetActive(true);
            
            var trophyContainerScript = trophyContainer.GetComponent<TrophyContainer>();
            trophyContainerScript.TrophyDescription.text = trophy.TrophySO.trophyDescription;
            
            switch (trophy.TrophyCompletionState)
            {
                case TrophyManager.CompletionState.NotComplete:
                    StartCoroutine(ShowCompletionBar(trophyContainerScript.TrophyProgression.gameObject.transform, trophy, trophyContainerScript.StartPosX));
                    break;
                case TrophyManager.CompletionState.Complete:
                    trophyContainerScript.TrophyLock.SetActive(false);
                    trophyContainerScript.RewardButton.SetActive(true);
                    trophyContainerScript.CompleteTag.SetActive(true);
                    trophyContainerScript.TrophyIcon.SetActive(true);
                    break;
                case TrophyManager.CompletionState.Claimed:
                    trophyContainerScript.TrophyLock.SetActive(false);
                    trophyContainerScript.RewardButton.SetActive(false);
                    trophyContainerScript.CompleteTag.SetActive(true);
                    trophyContainerScript.TrophyIcon.SetActive(true);
                    break;
            }
        }
    }
    
    private static IEnumerator ShowCompletionBar(Transform pos, TrophyManager.Trophy trophy, float startPos)
    {
        while (pos.position.x < startPos + 1.5f * (float)trophy.CurrentAmount / trophy.Goal)
        {
            if (trophy.CurrentAmount - trophy.Goal >= trophy.Goal)
            {
                pos.position += pos.right * (1.5f * Time.deltaTime);
            }
            else
            {
                pos.position += pos.right * ((float)trophy.CurrentAmount / trophy.Goal * 1.5f * Time.deltaTime);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void SetAmounts()
    {
        var currentAmount = _mTrophyManager.TrophyList.Count(trophy => trophy.TrophyCompletionState is TrophyManager.CompletionState.Complete or TrophyManager.CompletionState.Claimed);
        _currentAmount.text = currentAmount.ToString();
        _maxAmount.text = "/" + _mTrophyManager.TrophyList.Count;
    }
}
