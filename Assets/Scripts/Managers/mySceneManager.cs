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
    [Scene] public string HomeScreen;

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
        
        if (SceneManager.sceneCount < 4)
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
        else
            Debug.Log("Tried to duplicate scene");
    }

    public void UnloadCurrentScene()
    {
        SceneManager.UnloadSceneAsync(_mSceneName);
    }

    public void UnloadPreciseScene(string name)
    {
        SceneManager.UnloadSceneAsync(name);
    }

    public void LoadWinScreen()
    {
        SceneManager.LoadScene(WinScreen, LoadSceneMode.Single);
    }

    public void LoadFasterScreen()
    {
        SceneManager.LoadScene(FasterScreen, LoadSceneMode.Additive);
    }

    public void LoadHomeScreen()
    {
        SceneManager.LoadScene(HomeScreen, LoadSceneMode.Additive);
    }

    public void RandomGameChoice()
    {
        do
        {
            _mMinigameScene = MiniGameSelector.GetRandomElement(MiniGameSelector.instance.AllMinigames[GameManager.instance.Era]);
            Debug.Log(_mSceneName + "previous minigamge" + _mMinigameScene.SceneName);
        } while (_mMinigameScene.Locked || _mSceneName == _mMinigameScene.SceneName);

        SetScene(_mMinigameScene.SceneName, LoadMode.ADDITIVE);
    }
}
