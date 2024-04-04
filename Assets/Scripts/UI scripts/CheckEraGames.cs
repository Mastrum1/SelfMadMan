using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    private void Start()
    {
        _mUnlockedMinigames = 0;
        _mCurrentEra = GameManager.instance.Era;
        switch (_mCurrentEra)
        {
            case 0:
                CheckUnlockedMinigames(MiniGameSelector.instance.Era1, _mMinigameContainer);
                Debug.Log("Loading Era 1");
                break;
            case 1:
                CheckUnlockedMinigames(MiniGameSelector.instance.Era2, _mMinigameContainer);
                Debug.Log("Loading Era 2");
                break;
            case 2:
                CheckUnlockedMinigames(MiniGameSelector.instance.Era3, _mMinigameContainer);
                Debug.Log("Loading Era 3");
                break;
        }
        _mNumberUnlocked.text = _mUnlockedMinigames.ToString();
    }

    public void CheckUnlockedMinigames(List<MinigameScene> minigame, GameObject container)
    {
        for (int i = 0; i < minigame.Count; i++) 
        {
            GameObject Templates = Instantiate(_mGameContainerPrefab, container.transform);
            CheckEraGamesTemplate TemplateInfos = Templates.GetComponent<CheckEraGamesTemplate>();
            TemplateInfos.EraGameName.text = minigame[i].SceneName;
            //TemplateInfos.EraGameIcon.sprite = minigame[i].SceneIcon;

            if (minigame[i].Unlocked)
            {
                TemplateInfos.GameLocked = false;
                TemplateInfos.EraGameBorder = TemplateInfos.EraGameBorderUnlocked;
                _mUnlockedMinigames++;
            }
            else
            {
                TemplateInfos.GameLocked = true;
                TemplateInfos.EraGameBorder = TemplateInfos.EraGameBorderLocked;
            }
        }
    }
}
