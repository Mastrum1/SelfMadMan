using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckEraGames : MonoBehaviour
{
    [Header("Container info")]
    [SerializeField] private GameObject _mMinigameContainer;

    [Header("Prefab container")]
    [SerializeField] private GameObject _mGameContainerPrefab;

    [Header("Era Info")]
    [SerializeField] private int _mCurrentEra;

    private void Awake()
    {
        _mCurrentEra = GameManager.instance.Era;
    }

    public void CheckUnlockedMinigames()
    {
        
    }
}
