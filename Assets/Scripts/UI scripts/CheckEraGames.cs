using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    [SerializeField] private MinigameSO[] _mEra1;
    [SerializeField] private MinigameSO[] _mEra2;
    [SerializeField] private MinigameSO[] _mEra3;

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

    public void CheckUnlockedMinigames(MinigameSO[] Era)
    {
        for (int i = 0; i < Era.Length; i++) 
        { 
            if (Era == _mEra1)
            {
                foreach (var mini in MiniGameSelector.instance.Era1)
                {
                    if (_mEra1[i].MinigameScene == mini.SceneName)
                    {
                        _mMinigameContainerList[i].EraGameName.text = _mEra1[i].ItemName;
                        _mMinigameContainerList[i].EraGameIcon.sprite = _mEra1[i].Icon;

                        if (!mini.Locked)
                        {
                            _mMinigameContainerList[i].QuestIconBG.color = _mBGIconColors[0];
                            _mMinigameContainerList[i].EraGameName.color = _mBGIconColors[2];
                            _mMinigameContainerList[i].EraGameIcon.color = _mBGIconColors[2];
                            _mUnlockedMinigames++;
                        }

                        else if (mini.Locked)
                        {
                            _mMinigameContainerList[i].QuestIconBG.color = _mBGIconColors[1];
                            _mMinigameContainerList[i].EraGameName.color = _mBGIconColors[3];
                            _mMinigameContainerList[i].EraGameIcon.color = _mBGIconColors[3];
                        }
                    }
                }
            }

            if (Era == _mEra2)
            {
                foreach (var mini in MiniGameSelector.instance.Era2)
                {
                    if (_mEra2[i].MinigameScene == mini.SceneName)
                    {
                        _mMinigameContainerList[i].EraGameName.text = _mEra2[i].ItemName;
                        _mMinigameContainerList[i].EraGameIcon.sprite = _mEra2[i].Icon;

                        _mMinigameContainerList[7].EraGameName.text = "Coming soon";
                        _mMinigameContainerList[7].EraGameIcon.sprite = null;

                        if (!mini.Locked)
                        {
                            _mMinigameContainerList[i].QuestIconBG.color = _mBGIconColors[0];
                            _mMinigameContainerList[i].EraGameName.color = _mBGIconColors[2];
                            _mMinigameContainerList[i].EraGameIcon.color = _mBGIconColors[2];
                            _mUnlockedMinigames++;
                        }

                        else if (mini.Locked)
                        {
                            _mMinigameContainerList[i].QuestIconBG.color = _mBGIconColors[1];
                            _mMinigameContainerList[i].EraGameName.color = _mBGIconColors[3];
                            _mMinigameContainerList[i].EraGameIcon.color = _mBGIconColors[3];
                        }
                    }
                }
            }

            if (Era == _mEra3)
            {
                foreach (var mini in MiniGameSelector.instance.Era3)
                {
                    if (_mEra3[i].MinigameScene == mini.SceneName)
                    {
                        _mMinigameContainerList[i].EraGameName.text = _mEra3[i].ItemName;
                        _mMinigameContainerList[i].EraGameIcon.sprite = _mEra3[i].Icon;
                        
                        if (!mini.Locked)
                        {
                            _mMinigameContainerList[i].QuestIconBG.color = _mBGIconColors[0];
                            _mMinigameContainerList[i].EraGameName.color = _mBGIconColors[2];
                            _mMinigameContainerList[i].EraGameIcon.color = _mBGIconColors[2];
                            _mUnlockedMinigames++;
                        }

                        else if (mini.Locked)
                        {
                            _mMinigameContainerList[i].QuestIconBG.color = _mBGIconColors[1];
                            _mMinigameContainerList[i].EraGameName.color = _mBGIconColors[3];
                            _mMinigameContainerList[i].EraGameIcon.color = _mBGIconColors[3];
                        }
                    }
                }
            }
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
