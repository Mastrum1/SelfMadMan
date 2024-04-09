using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckEraGames : MonoBehaviour
{
    [Header("Container info")]
    [SerializeField] private GameObject _mMinigameContainer;

    [Header("Prefab container")]
    [SerializeField] private GameObject _mGameContainerPrefab;

    [Header("Era Info")]
    [SerializeField] private int _mCurrentEra;

    [Header("Unlocked Minigames")]
    [SerializeField] private TMP_Text _mNumberUnlocked;
    [SerializeField] private int _mUnlockedMinigames;

    [Header("Minigame Container List")]
    [SerializeField] private List<CheckEraGamesTemplate> _mMinigameContainerList;

    private void OnEnable()
    {
        CheckEra();
    }

    public void Checking()
    {
        _mUnlockedMinigames = 0;
        _mCurrentEra = GameManager.instance.Era;
        CheckEra();
    }

    public void CheckUnlockedMinigames(List<MinigameScene> minigame)
    {
        for (int i = 0; i < minigame.Count; i++)
        {
            _mMinigameContainerList[i].EraGameName.text = minigame[i].SceneName;
            Debug.Log("Current index : " + i);
            //minigameContainerScript.EraGameIcon.sprite = minigame[i].Icon;
            if (minigame[i].Locked)
            {
                _mMinigameContainerList[i].EraGameBorder.sprite = _mMinigameContainerList[i].EraGameBorderUnlocked;
                _mUnlockedMinigames++;
            }
            else
                _mMinigameContainerList[i].EraGameBorder.sprite = _mMinigameContainerList[i].EraGameBorderLocked;
        }
    }

    public void CheckEra()
    {
        switch (_mCurrentEra)
        {
            case 0:
                CheckUnlockedMinigames(MiniGameSelector.instance.Era1);
                break;
            case 1:
                CheckUnlockedMinigames(MiniGameSelector.instance.Era2);
                break;
            case 2:
                CheckUnlockedMinigames(MiniGameSelector.instance.Era3);
                break;
        }

        _mNumberUnlocked.text = _mUnlockedMinigames.ToString();
    }
}
