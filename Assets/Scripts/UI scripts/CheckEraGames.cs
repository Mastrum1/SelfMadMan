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

    [Header("Minigames SO")]
    [SerializeField] private List<MinigameSO> _mEra1;
    [SerializeField] private List<MinigameSO> _mEra2;
    [SerializeField] private List<MinigameSO> _mEra3;

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

    public void CheckUnlockedMinigames(List<MinigameSO> Era)
    {
        for (int i = 0; i < Era.Count; i++)
        {
            _mMinigameContainerList[i].EraGameName.text = Era[i].ItemName;
            _mMinigameContainerList[i].EraGameIcon.sprite = Era[i].Icon;

            
        }
    }

    public void CheckEra()
    {
        switch (_mCurrentEra)
        {
            case 0:
                CheckUnlockedMinigames(_mEra1);
                _mNumberUnlocked.text = $"<#4EF4F6><size=150%>{_mUnlockedMinigames}" + $"<#F6F3D1><size=100%>/{MiniGameSelector.instance.Era1.Count}";
                break;
            case 1:
                CheckUnlockedMinigames(_mEra2);
                _mNumberUnlocked.text = $"<#4EF4F6><size=150%>{_mUnlockedMinigames}" + $"<#F6F3D1><size=100%>/{MiniGameSelector.instance.Era2.Count}";
                break;
            case 2:
                CheckUnlockedMinigames(_mEra3);
                _mNumberUnlocked.text = $"<#4EF4F6><size=150%>{_mUnlockedMinigames}" + $"<#F6F3D1><size=100%>/{MiniGameSelector.instance.Era3.Count}";
                break;
        }
        _mEra.text = "ERA " + (_mCurrentEra + 1).ToString();
    }
}
