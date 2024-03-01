using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class mySceneManager : MonoBehaviour
{
    public static mySceneManager instance;

    private string _mScene;
    
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void SetScene(string scene)
    {
        _mScene = scene;
        SceneManager.LoadScene(_mScene, LoadSceneMode.Single);
    }

    public void RandomGameChoice(int era)
    {
        switch (era)
        {
            case 0:
                _mScene = MiniGameSelector.GetRandomElement(MiniGameSelector.instance.MiniGameSOsEra1);
                SetScene(_mScene);
                break;
            case 1:
                _mScene = MiniGameSelector.GetRandomElement(MiniGameSelector.instance.MiniGameSOsEra2);
                SetScene(_mScene);
                break;
            case 2:
                _mScene = MiniGameSelector.GetRandomElement(MiniGameSelector.instance.MiniGameSOsEra3);
                SetScene(_mScene);
                break;
            default:
                Debug.Log("Choose 1, 2 or 3");
                break;
        }
    }
}
