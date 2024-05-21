using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckEraGames : MonoBehaviour
{
    [Header("Container info")]
    [SerializeField] private GameObject _mMinigameContainer;

    [Header("Era Info")]
    [SerializeField] private int _mCurrentEra;

    [Header("Unlocked Minigames")]
    [SerializeField] private TMP_Text _mNumberUnlocked;
    [SerializeField] private int _mUnlockedMinigames;
    [SerializeField] private List<Color> _mBGIconColors;

    [Header("Current Era Minigames")]
    [SerializeField] private TMP_Text _mEra;

    [Header("Minigame Container List")]
    [SerializeField] private List<CheckEraGamesTemplate> _mMinigameContainerList;

    private void OnEnable()
    {
        Checking();
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
            _mMinigameContainerList[i].EraGameIcon.sprite = minigame[i].Icon;

            if (!minigame[i].Locked)
            {
                _mMinigameContainerList[i].QuestIconBG.color = _mBGIconColors[0];
                _mMinigameContainerList[i].EraGameName.color = _mBGIconColors[2];
                _mMinigameContainerList[i].EraGameIcon.color = _mBGIconColors[2];
                _mUnlockedMinigames++;
            }

            else
            {
                _mMinigameContainerList[i].QuestIconBG.color = _mBGIconColors[1];
                _mMinigameContainerList[i].EraGameName.color = _mBGIconColors[3];
                _mMinigameContainerList[i].EraGameIcon.color = _mBGIconColors[3];
            }  
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

        _mNumberUnlocked.text = $"<#4EF4F6><size=150%>{_mUnlockedMinigames}" + "<#F6F3D1><size=100%>/8";
        _mEra.text = "ERA " + (_mCurrentEra + 1).ToString();
    }
}
