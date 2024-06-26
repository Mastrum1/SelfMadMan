﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using static QuestManager;

public class MiniGameSelector : MonoBehaviour
{
    public static MiniGameSelector instance;

    public Dictionary<string, List<string>> Minigamelists { get => _mMinigameLists; private set => _mMinigameLists = value; }
    private Dictionary<string, List<string>> _mMinigameLists = new Dictionary<string, List<string>>();

    [SerializeField] private List<MinigameScene> _era1;
    [SerializeField] private List<MinigameScene> _era2;
    [SerializeField] private List<MinigameScene> _era3;

    [SerializeField] private List<MinigameScene> _SavedEra1;
    [SerializeField] private List<MinigameScene> _SavedEra2;
    [SerializeField] private List<MinigameScene> _SavedEra3;


    public List<MinigameScene> Era1 { get => _era1; set => _era1 = value; }
    public List<MinigameScene> Era2 { get => _era2; set => _era2 = value; }
    public List<MinigameScene> Era3 { get => _era3; set => _era3 = value; }

    private List<List<MinigameScene>> _allMinigames = new List<List<MinigameScene>>();
    public List<List<MinigameScene>> AllMinigames { get => _allMinigames; set => _allMinigames = value; }

    Regex regex = new Regex(@"([^/]*/)*([\w\d\-]*)\.unity");

    private void Start()
    {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        if (GameManager.instance.Player.LoadSaveMinigame)
        {
            Era1 = GameManager.instance.Player.AllEra1;
            Era2 = GameManager.instance.Player.AllEra2;
            Era3 = GameManager.instance.Player.AllEra3;
        }

        _allMinigames.Add(Era1);
        _allMinigames.Add(Era2);
        _allMinigames.Add(Era3);
    }


    public static T GetRandomElement<T>(List<T> list)
    {
        int index = UnityEngine.Random.Range(0, list.Count);
        Debug.Log(index);
        return list[index];
    }

    public void UnlockMinigame(string SceneName)
    {

        for (int i = 0; i < AllMinigames.Count; i++)
        {
            for (int j = 0; j < AllMinigames[i].Count; j++)
            {
                if (AllMinigames[i][j].SceneName == SceneName)
                {
                    AllMinigames[i][j].Unlock();
                    GameManager.instance.GetComponent<Player>().UnlockMinigame(i, j);
                    TrophyManager.Instance.AddTrophyAmount(i,1);
                    break;
                }

            }
        }
    }

}
