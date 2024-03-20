using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class SceneManaProxy : MonoBehaviour
{
    List<string> _mMinigameNames = new List<string>();

    [Scene] public string SceneName;

    public int Era {  get => _mEra; private set => _mEra = value; }
    [SerializeField] private int _mEra;

    public void ChangeScene()
    {
        mySceneManager.instance.SetScene(SceneName, mySceneManager.LoadMode.SINGLE);
    }

    public void StartGame()
    {
        GameManager.instance.OnGameStart();
    }
}