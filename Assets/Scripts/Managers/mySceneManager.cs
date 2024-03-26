using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

using UnityEngine;

public class mySceneManager : MonoBehaviour
{
    public static mySceneManager instance;

    [Scene] public string WinScreen;
    [Scene] public string FasterScreen;

    private string _mSceneName;
    private MinigameScene _mMinigameScene;

    public enum LoadMode { SINGLE, ADDITIVE };

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void SetScene(string scene, LoadMode mode)
    {
        _mSceneName = scene;
        switch (mode)
        {
            case LoadMode.SINGLE:
                SceneManager.LoadScene(_mSceneName, LoadSceneMode.Single);
                break;
            case LoadMode.ADDITIVE:
                SceneManager.LoadScene(_mSceneName, LoadSceneMode.Additive);
                break;
        }

    }

    public void UnloadCurrentScene()
    {
        SceneManager.UnloadSceneAsync(_mMinigameScene.SceneName);
    }

    public void LoadWinScreen()
    {
        SceneManager.LoadScene(WinScreen, LoadSceneMode.Single);
    }

    public void LoadFasterScreen()
    {
        SceneManager.LoadScene(FasterScreen, LoadSceneMode.Additive);
    }

    public void RandomGameChoice()
    {
        do
        {
            _mMinigameScene = MiniGameSelector.GetRandomElement(MiniGameSelector.instance.AllMinigames[GameManager.instance.Era]);

        } while (_mMinigameScene.Unlocked);

        SetScene(_mMinigameScene.SceneName, LoadMode.ADDITIVE);
    }
}
