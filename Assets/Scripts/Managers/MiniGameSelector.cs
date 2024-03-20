using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameSelector : MonoBehaviour
{
    public static MiniGameSelector instance;

    public Dictionary<string, List<string>> Minigamelists {  get => _mMinigameLists; private set => _mMinigameLists = value; }
    private Dictionary<string, List<string>> _mMinigameLists = new Dictionary<string, List<string>>();

    [SerializeField] public List<string> Era1 = new List<string>(); // TO DO : change to dictionnary if not unlocked
    public List<string> Era2 = new List<string>();
    public List<string> Era3 = new List<string>();

    Regex regex = new Regex(@"([^/]*/)*([\w\d\-]*)\.unity");

    private void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

       // GetNamesScenes();
        //GetMinigamesNameEra();
        DontDestroyOnLoad(gameObject);
    }

    void GetNamesScenes()
    {
        string minigamesFolder = Application.dataPath + "/Scenes/Minigames";
        string[] minigameFolders = Directory.GetDirectories(minigamesFolder);

        foreach (string minigameFolder in minigameFolders)
        {
            List<string> sceneNames = new List<string>();

            var dirInfo = new DirectoryInfo(minigameFolder);
            var allFileInfos = dirInfo.GetFiles("*.unity", SearchOption.AllDirectories);

            foreach (var fileInfo in allFileInfos)
            {
                string sceneName = ExtractSceneName(fileInfo.FullName);
                sceneNames.Add(sceneName);
            }

            // Add the list of scene names to the dictionary with the minigame folder name as the key
            _mMinigameLists.Add(Path.GetFileName(minigameFolder), sceneNames);
        }
        // Now minigameLists contains a dictionary where keys are minigame folder names, and values are lists of scene names.
        // You can access the lists using minigameLists["FolderName"].
    }

    string ExtractSceneName(string fullPath)
    {
        Match match = regex.Match(fullPath);
        if (match.Success)
        {
            return match.Groups[2].Value; // Group 2 contains the scene name
        }
        else
        {
            // Handle the case where the match is not successful
            Debug.LogWarning("Unable to extract scene name for: " + fullPath);
            return fullPath;
        }
    }

    void GetMinigamesNameEra()
    {
        foreach (string minigameName in _mMinigameLists["Era1"])
        {
           Era1.Add(minigameName);
        }
            
        foreach (string minigameName in _mMinigameLists["Era2"])
        {
           Era2.Add(minigameName);
        }
              
        foreach (string minigameName in _mMinigameLists["Era3"])
        {
           Era3.Add(minigameName);
        }
    }

    public static T GetRandomElement<T>(List<T> list)
    {
        int index = UnityEngine.Random.Range(0, list.Count);
        Debug.Log(index);
        return list[index];
    }
}
