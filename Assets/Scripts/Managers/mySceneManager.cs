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

    private string _mScene;
    private string _mMinigameScene;

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
        _mScene = scene;
        switch (mode)
        {
            case LoadMode.SINGLE:
                SceneManager.LoadScene(_mScene, LoadSceneMode.Single);
                break;
            case LoadMode.ADDITIVE:
                SceneManager.LoadScene(_mScene, LoadSceneMode.Additive);
                break;
        }

    }

    public void UnloadCurrentScene()
    {
        SceneManager.UnloadSceneAsync(_mMinigameScene);
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
        switch (GameManager.instance.Era)
        {
            case 0:
                _mMinigameScene = MiniGameSelector.GetRandomElement<string>(MiniGameSelector.instance.Era1);

                break;
            case 1:
                _mMinigameScene = MiniGameSelector.GetRandomElement<string>(MiniGameSelector.instance.Era2);

                break;
            case 2:
                _mMinigameScene = MiniGameSelector.GetRandomElement<string>(MiniGameSelector.instance.Era3);
                break;
            default:
                Debug.Log("Choose 1, 2 or 3");
                break;
        }
        SetScene(_mMinigameScene, LoadMode.ADDITIVE);
    }
}
