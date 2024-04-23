using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class SceneManaProxy : MonoBehaviour
{
    List<string> _mMinigameNames = new List<string>();

    [Scene] public string SceneName;

    public int Era {  get => _mEra; private set => _mEra = value; }
    [SerializeField] private int _mEra;

    public void ChangeScene()
    {
        if (!mySceneManager.instance)
            SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
        else if (mySceneManager.instance != null)
            mySceneManager.instance.SetScene(SceneName, mySceneManager.LoadMode.SINGLE);

    }

    public void StartGame()
    {
        GameManager.instance.OnGameStart();
    }

    // When mySceneManager is not avaible
    public void MinigamesChoice()
    {
        SceneManager.LoadScene("MinigamesChoice", LoadSceneMode.Single);
    }
}