using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameSelector : MonoBehaviour
{
    public static MiniGameSelector instance;

    public List<MiniGameSO> AllMiniGamesSOs = new List<MiniGameSO>();

    public List<string> MiniGameSOsEra1 = new List<string>();
    public List<string> MiniGameSOsEra2 = new List<string>();
    public List<string> MiniGameSOsEra3 = new List<string>();

    Regex regex = new Regex(@"([^/]*/)*([\w\d\-]*)\.unity");

    private void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        LoadMinigameSO();
        CategorizeMiniSO();
        //GetNamesScenes();
        //GetMinigamesNameEra();
        DontDestroyOnLoad(gameObject);
    }

    void LoadMinigameSO()
    {
        MiniGameSO[] miniGameSOs = Resources.LoadAll<MiniGameSO>("");

        foreach (MiniGameSO miniGameSO in miniGameSOs) 
        { 
            AllMiniGamesSOs.Add(miniGameSO);
        }
    }

    void CategorizeMiniSO()
    {
        foreach (MiniGameSO miniGameSO in AllMiniGamesSOs) 
        {
            switch (miniGameSO.MinigameEra) 
            {
                case 1:
                    MiniGameSOsEra1.Add(miniGameSO.MinigameName);
                    break;
                case 2:
                    MiniGameSOsEra2.Add(miniGameSO.MinigameName);
                    break;
                case 3:
                    MiniGameSOsEra3.Add(miniGameSO.MinigameName);
                    break;
                default:
                    Debug.Log("nop");
                    break;
            }
        }
    }

    public static T GetRandomElement<T>(List<T> list)
    {
        int index = UnityEngine.Random.Range(0, list.Count);
        Debug.Log(index);
        return list[index];
    }
}
